using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;

namespace Authentication.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override Caerus.Common.Enums.ModuleTypes ModuleId
        {
            get { return ModuleTypes.Authentication; }
        }

       
    }
}
