using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Lookup.Enums;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Lookup.ViewModels
{
    public class LookupListViewModel : ReplyObject
    {
        public List<LookupViewModel> LookupList { get; set; }
        public LookupTypes LookupType { get; set; }

        public LookupListViewModel()
        {
            LookupList = new List<LookupViewModel>();
        }
    }

}
