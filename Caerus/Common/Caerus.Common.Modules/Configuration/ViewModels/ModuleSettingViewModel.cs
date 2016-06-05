using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Configuration.ViewModels
{
    public class ModuleSettingViewModel
    {
        public int ModuleId { get; set; }
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        public bool SettingBoolValue { get; set; }
        public string UserChanged { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
