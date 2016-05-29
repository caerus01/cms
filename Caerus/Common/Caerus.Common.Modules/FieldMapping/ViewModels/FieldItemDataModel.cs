using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Enums;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class FieldItemDataModel
    {
        public int OwningEntityType { get; set; }
        public dynamic Value { get; set; }
        public string FieldId { get; set; }
        public SystemDataTypes SystemDataType { get; set; }
    }
}
