using System.Data.Entity;
using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Audit.Entities;

namespace Caerus.Modules.Audit.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override Caerus.Common.Enums.ModuleTypes ModuleId
        {
            get { return ModuleTypes.Audit; }
        }

        public DbSet<HttpRequestLog> HttpRequestLogs { get; set; } 
    }
}
