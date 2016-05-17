using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
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
