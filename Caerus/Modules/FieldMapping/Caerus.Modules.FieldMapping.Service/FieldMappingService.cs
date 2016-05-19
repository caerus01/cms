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

        private List<FieldItemDataModel> GetEntityFields(long owningEntityRef, List<FieldDisplaySetup> requiredFields, List<DynamicEntityViewModel> entities)
        {
            var result = new List<FieldItemDataModel>();
            try
            {

                foreach (var item in entities)
                {
                    var fields = requiredFields.Where(c => c.OwningType == item.OwningEntityType).ToList();
                    foreach (var fitem in fields)
                    {
                        var model = new FieldItemDataModel()
                        {
                            FieldId = fitem.FieldId,
                            OwningEntityType = fitem.OwningEntityType,
                            SystemDataType = GetDataType(item.EntityType, fitem.FieldId)
                        };

                        var prop = item.GetType().GetProperty(fitem.FieldId);
                        if (prop != null)
                            model.Value = prop.GetValue(item).ToString();
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
                var requiredEntityTypes = fields.Select(c => c.OwningEntityType).Distinct().ToList();
                var entityFields = ResolveServiceFromType(entityType).GetEntityModelsByTypes(requiredEntityTypes, owningEntityRef);
                var entityData = GetEntityFields(owningEntityRef, fields, entityFields);
                foreach (var item in fields)
                {
                    var model = new FieldItemModel();
                    item.CopyProperties(model);
                    var data =
                        entityData.FirstOrDefault(
                            c => c.FieldId == item.FieldId && c.OwningEntityType == item.OwningEntityType);
                    if (data != null)
                        model.FieldValue = data.Value;
                }

            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }
        #endregion

        public DynamicFieldReplyViewModel GetEntityFieldsByRank(OwningTypes entityType, long owningEntityRef, FieldRanks fieldRank)
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

                return ResolveServiceFromType(viewModel.OwningType).SaveEntityFields(viewModel);
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
                foreach (var field in viewModel.Fields)
                {
                    foreach (var validation in field.FieldValidations)
                    {
                        if (!IsValid(validation.ValidationType, field.FieldValue, validation.ValidationValue))
                            result.Errors.Add(validation.ValidationMessage);
                    }
                }
                if (result.Errors.Any())
                {
                    result.ReplyStatus = ReplyStatus.Warning;
                    result.ReplyMessage = "Field Validation Failed";
                }

                return ResolveServiceFromType(viewModel.OwningType).SaveEntityFields(viewModel);
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
                    case FieldValidationTypes.RangeLength: // – Makes the element require a given value length range.
                        var range = validationValue.Replace("[", string.Empty).Replace("]", string.Empty).Split(',');
                        return value.Length >= Convert.ToInt32(range[0]) && value.Length <= Convert.ToInt32(range[1]);
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
                    case FieldValidationTypes.RsaIdNumber:
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
