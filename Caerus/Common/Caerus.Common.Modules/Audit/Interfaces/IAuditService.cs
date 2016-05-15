using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Audit.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Audit.Interfaces
{
    public interface IAuditService
    {
        ReplyObject LogRequest(AuditRequestViewModel request);
    }
}
