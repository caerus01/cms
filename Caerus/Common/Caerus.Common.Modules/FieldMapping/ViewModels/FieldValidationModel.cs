using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Enums;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class FieldValidationModel
    {
        public string FieldId { get; set; }
        public FieldValidationTypes ValidationType { get; set; }
        public string ValidationValue { get; set; }
        public string ValidationMessage { get; set; }
    }
}
