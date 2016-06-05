using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Configuration.Entities;

namespace Caerus.Common.Modules.Configuration.Interfaces
{
    public interface IConfigurationRepository : IRepository
    {
        bool SetModuleSetting(ModuleTypes moduleId, int settingId, string value, string userId);
        string GetModuleSetting(ModuleTypes moduleId, int settingId);
        void AddModuleConfiguration(ModuleConfiguration item);
        int GetServiceModuleInUse(ModuleTypes moduleType);
    }
}
