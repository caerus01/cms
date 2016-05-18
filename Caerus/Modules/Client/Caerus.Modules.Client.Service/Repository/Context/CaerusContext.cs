using System.Data.Entity;
using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Audit.Entities;

namespace Caerus.Modules.Client.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override ModuleTypes ModuleId
        {
            get { return ModuleTypes.Client; }
        }
    }
}
