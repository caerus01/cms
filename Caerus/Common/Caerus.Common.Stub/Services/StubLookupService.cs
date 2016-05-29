using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Lookup.Interfaces;
using Caerus.Common.Modules.Lookup.ViewModels;

namespace Caerus.Common.Stub.Services
{
    public class StubLookupService : ILookupService
    {
        public LookupListViewModel GetLookupList(Modules.Lookup.Enums.LookupTypes lookupType, string filter = "", int recordCount = 0, long? key = null)
        {
            return new LookupListViewModel();
        }
    }
}
