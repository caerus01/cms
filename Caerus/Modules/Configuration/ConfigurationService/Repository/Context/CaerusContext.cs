using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;
using Caerus.Common.Interfaces;
using Caerus.Common.Modules.Configuration.Entities;

namespace Caerus.Modules.Configuration.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override Caerus.Common.Enums.ModuleTypes ModuleId
        {
            get { return ModuleTypes.Configuration; }
        }

        public DbSet<ModuleConfiguration> ModuleConfigurations { get; set; }

        public DbSet<ModuleSetting> ModuleSettings { get; set; }

        public bool SetModuleSetting(ModuleTypes moduleId, int settingId, string value, string userId)
        {
            var module = new SqlParameter("@Module", (int)moduleId);
            var setting = new SqlParameter("@Setting", settingId);
            var val = new SqlParameter("@Value", value);
            var user = new SqlParameter("@User", userId);

            var result = Database.SqlQuery<string>("SetModuleSetting @Module, @Setting, @Value, @User",
                      module, setting, val, user).FirstOrDefault();

            return result == "Success";
        }
    }
}
