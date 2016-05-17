using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class FieldItemModel : ReplyObject
    {
        public FieldItemModel()
        {
            FieldValidations = new List<FieldValidationModel>();
        }

        public List<FieldValidationModel> FieldValidations { get; set; }
        public string FieldValue { get; set; }

    }
}
