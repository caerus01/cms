using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Audit.Interfaces;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Services
{
    public class StubAuditService : IAuditService
    {
        public ViewModels.ReplyObject LogRequest(Modules.Audit.ViewModels.AuditRequestViewModel request)
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }
    }
}
