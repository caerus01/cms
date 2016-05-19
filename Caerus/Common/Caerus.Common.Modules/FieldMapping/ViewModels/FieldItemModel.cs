using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Client.Enums;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.Lookup.Enums;
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

        public OwningTypes OwningType { get; set; }
        public int OwningEntityType { get; set; }
        public int View { get; set; }
        public int Sequence { get; set; }
        public string FieldId { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
        public FieldTypes FieldType { get; set; }
        public string CssClass { get; set; }
        public FieldRanks FieldRank { get; set; }
        public LookupTypes LookupType { get; set; }
        public SystemDataTypes SystemDataType { get; set; }
        public bool ReadOnly { get; set; }
        public string FieldMask { get; set; }

    }
}
