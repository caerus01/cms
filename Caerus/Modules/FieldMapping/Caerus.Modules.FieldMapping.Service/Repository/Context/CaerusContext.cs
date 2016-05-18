using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;

namespace Caerus.Modules.FieldMapping.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override ModuleTypes ModuleId
        {
            get { return ModuleTypes.FieldMapping; }
        }
    }
}
