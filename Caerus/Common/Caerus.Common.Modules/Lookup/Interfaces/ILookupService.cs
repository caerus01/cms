using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Lookup.Enums;
using Caerus.Common.Modules.Lookup.ViewModels;

namespace Caerus.Common.Modules.Lookup.Interfaces
{
    public interface ILookupService
    {
        LookupListViewModel GetLookupList(LookupTypes lookupType, string filter = "", int recordCount = 0, long? key = null);
    }
}
