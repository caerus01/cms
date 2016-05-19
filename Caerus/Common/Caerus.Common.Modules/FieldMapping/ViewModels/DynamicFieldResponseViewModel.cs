using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.Lookup.ViewModels;

namespace Caerus.Common.Modules.FieldMapping.ViewModels
{
    public class DynamicFieldResponseViewModel
    {
        public DynamicFieldResponseViewModel()
        {
            Fields = new List<FieldItemModel>();
            Lookups = new List<LookupListViewModel>();
        }
        public List<FieldItemModel> Fields { get; set; }
        public OwningTypes OwninType { get; set; }
        public long OwningEntityRef { get; set; }

        public List<LookupListViewModel> Lookups { get; set; } 
    }
}
