using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Modules.Audit.Entities;

namespace Caerus.Common.Modules.Audit.Interfaces
{
    public interface IAuditRepository : IRepository
    {
        void AddRequestLog(HttpRequestLog item);
    }
}
