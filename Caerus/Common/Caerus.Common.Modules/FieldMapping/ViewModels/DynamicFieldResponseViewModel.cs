using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class DynamicFieldResponseViewModel
    {
        public List<FieldItemModel> Fields { get; set; }
        public int OwningEntityType { get; set; }
        public long OwningEntityRef { get; set; }
    }
}
