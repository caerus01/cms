using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.Stub.BaseServices;
using Caerus.Common.Stub.Services;
using Caerus.Common.ViewModels;
using Caerus.Modules.FieldMapping.Service.Repository;
using Caerus.Modules.FieldMapping.Service.Tools;

namespace Caerus.Modules.FieldMapping.Service
{

    public class FieldMappingService : IFieldMappingService
    {
        private readonly ICaerusSession _session;
        private readonly IFieldMappingRepository _repository;
        public FieldMappingService(ICaerusSession session, IFieldMappingRepository repository = null)
        {
            _session = session;
            _repository = repository ?? new FieldMappingRepository();
        }

        #region Private Members
        private IDynamicService ResolveServiceFromType(OwningTypes entityType)
        {
            switch (entityType)
            {
                case OwningTypes.Client:
                    {
                        return _session.ClientService;
                    }
                default:
                    {
                        return new StubDynamicService();
                    }
            }
        }

        private SystemDataTypes GetDataType(Type sourceType, string fieldName)
        {
            foreach (var sourceProp in sourceType.GetProperties().Where(sourceProp => sourceProp.Name == fieldName))
            {
                if (sourceProp.PropertyType == typeof(string))
                    return SystemDataTypes.String;

                if (sourceProp.PropertyType == typeof(decimal) || sourceProp.PropertyType == typeof(decimal?))
                    return SystemDataTypes.Decimal;

                if (sourceProp.PropertyType == typeof(int) || sourceProp.PropertyType == typeof(int?))
                    return SystemDataTypes.Interger;

                if (sourceProp.PropertyType == typeof(long) || sourceProp.PropertyType == typeof(long?))
                    return SystemDataTypes.Interger64;

                if (sourceProp.PropertyType == typeof(DateTime) || sourceProp.PropertyType == typeof(DateTime?))
                    return SystemDataTypes.DateTime;
            }

            return SystemDataTypes.Unknown;
        }

        private List<FieldItemDataModel> GetEntityFields(List<FieldDisplaySetup> requiredFields, List<DynamicEntityViewModel> entities)
        {
            var result = new List<FieldItemDataModel>();
            try
            {

                foreach (var item in entities)
                {
                    var fields = requiredFields.Where(c => c.OwningEntityType == item.OwningEntityType).ToList();
                    foreach (var fitem in fields)
                    {
                        var model = new FieldItemDataModel()
                        {
                            FieldId = fitem.FieldId,
                            OwningEntityType = fitem.OwningEntityType,
                            SystemDataType = GetDataType(item.EntityType, fitem.FieldId)
                        };

                        if (item.EntityObject != null)
                        {
                            var prop = item.EntityObject.GetType().GetProperty(fitem.FieldId);
                            var val = "";
                            if (prop != null)
                            {
                                val = prop.GetValue(item.EntityObject) ?? "";
                            }
                            model.Value = val;
                        }

                        result.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        private DynamicFieldReplyViewModel ResolveDynamicFields(OwningTypes entityType, List<FieldDisplaySetup> fields, long owningEntityRef)
        {
            var result = new DynamicFieldReplyViewModel();
            try
            {
                result.OwningType = entityType;
                result.OwningEntityRef = owningEntityRef;
                var requiredEntityTypes = fields.Select(c => c.OwningEntityType).Distinct().ToList();
                var validations = _repository.GetFieldValidationsByEntity(entityType, requiredEntityTypes);
                var entityFields = ResolveServiceFromType(entityType).GetEntityModelsByTypes(requiredEntityTypes, owningEntityRef);
                var entityData = GetEntityFields(fields, entityFields);
                foreach (var item in fields)
                {
                    var model = new FieldItemModel();
                    item.CopyProperties(model);
                    var data =
                        entityData.FirstOrDefault(
                            c => c.FieldId == item.FieldId && c.OwningEntityType == item.OwningEntityType);
                    if (data != null)
                        model.FieldValue = data.Value;


                    var vals = validations.Where(c => c.OwningEntityType == item.OwningEntityType).ToList();
                    foreach (var fieldValidation in vals)
                    {
                        model.FieldValidations.Add(new FieldValidationModel()
                        {
                            FieldId = item.FieldId,
                            ValidationMessage = fieldValidation.ValidationMessage,
                            ValidationType = (FieldValidationTypes)fieldValidation.ValidationType
                        });
                    }

                    result.Fields.Add(model);
                }

            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }
        #endregion

        public DynamicFieldReplyViewModel GetEntityFieldsByRank(OwningTypes entityType, long owningEntityRef, int fieldRank)
        {
            var result = new DynamicFieldReplyViewModel();
            try
            {
                var fields = _repository.GetEntityFieldsByRank(entityType, fieldRank);
                return ResolveDynamicFields(entityType, fields, owningEntityRef);
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByView(OwningTypes entityType, long owningEntityRef, int view)
        {
            var result = new DynamicFieldReplyViewModel();
            try
            {
                var fields = _repository.GetEntityFieldsByView(entityType, view);
                return ResolveDynamicFields(entityType, fields, owningEntityRef);
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByEntityType(OwningTypes entityType, long owningEntityRef, int owningEntityType)
        {
            var result = new DynamicFieldReplyViewModel();
            try
            {
                var fields = _repository.GetEntityFieldsByEntityType(entityType, owningEntityType);
                return ResolveDynamicFields(entityType, fields, owningEntityRef);
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public ReplyObject SaveEntityFields(DynamicFieldReplyViewModel viewModel)
        {
            var result = new DynamicFieldReplyViewModel();
            try
            {
                var validate = IsModelValid(viewModel);
                if (validate.ReplyStatus != ReplyStatus.Success)
                    return validate;

                var entities = viewModel.Fields.Select(c => c.OwningEntityType).Distinct();
                var model = new List<DynamicResponseDataModel>();
                foreach (var item in entities)
                {
                    var itemModel = new DynamicResponseDataModel()
                    {
                        OwningEntityType = item
                    };
                    var fields = viewModel.Fields.Where(c => c.OwningEntityType == item).ToList();
                    foreach (var fItem in fields)
                    {
                        itemModel.Fields.Add(fItem.FieldId, fItem.FieldValue);
                    }
                    model.Add(itemModel);
                }
                return ResolveServiceFromType(viewModel.OwningType).SaveEntityFields(viewModel.OwningEntityRef, model);
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public ReplyObject IsModelValid(DynamicFieldReplyViewModel viewModel)
        {
            var result = new ReplyObject();
            try
            {
                var qry = from t in viewModel.Fields
                           select new FieldEntityViewModel
                           {
                               OwningEntityType = t.OwningEntityType,
                               FieldId = t.FieldId
                           };
                var list = qry.ToList();
                var validations = _repository.GetFieldValidationsByEntityAndField(viewModel.OwningType, list);

                foreach (var field in viewModel.Fields)
                {
                    foreach (var validation in validations.Where(c => c.OwningEntityType == field.OwningEntityType && c.FieldId == field.FieldId).ToList())
                    {
                        if (!IsValid((FieldValidationTypes)validation.ValidationType, field.FieldValue, validation.ValidationValue))
                            result.Errors.Add(validation.ValidationMessage);
                    }
                }
                if (result.Errors.Any())
                {
                    result.ReplyStatus = ReplyStatus.Warning;
                    result.ReplyMessage = "Field Validation Failed";
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public ReplyObject AssignFields(Dictionary<string, dynamic> fields, dynamic entity, Type entityType)
        {
            var result = new ReplyObject();
            try
            {
                foreach (var fitem in fields)
                {
                    var prop = entityType.GetProperty(fitem.Key);
                    if (prop != null)
                        PropertyExtentions.SafeSetProperty(prop, entity, fitem.Value);
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public NextOutstandingCheckViewModel GetNextOutstandingEntityByRank(OwningTypes entityType, long owningEntityRef, int maxRankCheck = 2)
        {
            var result = new NextOutstandingCheckViewModel();
            try
            {
                for (int i = 1; i <= maxRankCheck; i++)
                {
                    var model = GetEntityFieldsByRank(entityType, owningEntityRef, i);
                    var isValid = IsModelValid(model);
                    if (isValid.ReplyStatus != ReplyStatus.Success)
                    {

                        isValid.CopyProperties(result);
                        result.OutstandingRank = i;
                        result.ReplyMessage = "Outstanding entity at rank " + i;
                        return result;
                    }

                }

            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public bool IsValid(FieldValidationTypes type, string value, string validationValue)
        {
            try
            {
                switch (type)
                {
                    case FieldValidationTypes.Required:
                        return (!string.IsNullOrWhiteSpace(value));
                    case FieldValidationTypes.MinLength: // – Makes the element require a given minimum length.
                        return value.Length >= Convert.ToInt32(validationValue);
                    case FieldValidationTypes.MaxLength: // – Makes the element require a given maxmimum length.
                        return value.Length <= Convert.ToInt32(validationValue);
                    case FieldValidationTypes.MinValue: // – Makes the element require a given minimum.
                        return Convert.ToDecimal(value) >= Convert.ToDecimal(validationValue);
                    case FieldValidationTypes.MaxValue: // – Makes the element require a given maximum.
                        return Convert.ToDecimal(value) <= Convert.ToDecimal(validationValue);
                    case FieldValidationTypes.Range: // – Makes the element require a given value range.
                        var range2 = validationValue.Replace("[", string.Empty).Replace("]", string.Empty).Split(',');
                        return Convert.ToDecimal(value) >= Convert.ToDecimal(range2[0]) && Convert.ToDecimal(value) <= Convert.ToDecimal(range2[1]);
                    case FieldValidationTypes.Email: // – Makes the element require a valid email
                        return Regex.IsMatch(value, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", RegexOptions.IgnoreCase);
                    case FieldValidationTypes.Url: // – Makes the element require a valid url
                        return Regex.IsMatch(value, @"^(http(?:s)?\:\/\/[a-zA-Z0-9\-]+(?:\.[a-zA-Z0-9\-]+)*\.[a-zA-Z]{2,6}(?:\/?|(?:\/[\w\-]+)*)(?:\/?|\/\w+\.[a-zA-Z]{2,4}(?:\?[\w]+\=[\w\-]+)?)?(?:\&[\w]+\=[\w\-]+)*)$", RegexOptions.IgnoreCase);
                    case FieldValidationTypes.Date: // – Makes the element require a date.
                        DateTime temp;
                        return (DateTime.TryParse(value, out temp));
                    case FieldValidationTypes.Number: // – Makes the element require a decimal number.
                        decimal temp3;
                        return Decimal.TryParse(value, out temp3);
                    case FieldValidationTypes.Digits: // – Makes the element require digits only.
                        return Regex.IsMatch(value, @"^[0-9]+$", RegexOptions.IgnoreCase);
                    case FieldValidationTypes.CreditCard: // – Makes the element require a credit card number.
                        return Regex.IsMatch(value, @"((6011)|(4\d{3})|(5[1-5]\d{2}))[ -]?(\d{4}[ -]?){3}", RegexOptions.IgnoreCase);
                    case FieldValidationTypes.EqualTo: // – Requires the element to be the same as another one
                        return value.Equals(validationValue);
                    case FieldValidationTypes.SpecificLength: // - Specific Length
                        return value.Length == Convert.ToInt32(validationValue);
                    case FieldValidationTypes.MaxAge:
                        {
                            DateTime dob;
                            int maxAge = 0;
                            DateTime.TryParse(value, out dob);
                            int.TryParse(validationValue, out maxAge);
                            return dob.GetAge() <= maxAge;
                        }
                    case FieldValidationTypes.MinAge:
                        {
                            DateTime dob;
                            int minAge = 0;
                            DateTime.TryParse(value, out dob);
                            int.TryParse(validationValue, out minAge);
                            return dob.GetAge() >= minAge;
                        }
                    case FieldValidationTypes.MinMonths:
                        {
                            DateTime currentDate;
                            DateTime.TryParse(value, out currentDate);
                            int minMonth = 0;
                            int.TryParse(validationValue, out minMonth);
                            return DateTime.Now.AddMonths(minMonth) <= currentDate;
                        }
                    case FieldValidationTypes.MaxMonths:
                        {
                            DateTime currentDate;
                            DateTime.TryParse(value, out currentDate);
                            int maxMonth = 0;
                            int.TryParse(validationValue, out maxMonth);
                            return DateTime.Now.AddMonths(maxMonth) >= currentDate;
                        }
                    case FieldValidationTypes.Regex:
                        return Regex.IsMatch(value, validationValue);
                    case FieldValidationTypes.IdentificationNumber:
                        return IdNumberTools.IdNumberValid(value);
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex, new dynamic[] { type, value, validationValue });
            }

            return false;
        }

    }
}
