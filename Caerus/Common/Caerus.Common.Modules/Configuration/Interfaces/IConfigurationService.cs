using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Configuration.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Configuration.Interfaces
{
    public interface IConfigurationService
    {
        bool IsModuleInUse(ModuleTypes module, int type);
        bool SetModuleSetting(ModuleTypes moduleId, int settingId, string value);
        T GetModuleSetting<T>(ModuleTypes type, int settingId);
        ReplyObject SaveModuleSetting(ModuleSettingViewModel model);
    }
}
