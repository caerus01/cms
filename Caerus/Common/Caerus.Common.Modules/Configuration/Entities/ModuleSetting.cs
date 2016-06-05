using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Configuration.Entities
{
    [Table("ModuleSettings")]
    public class ModuleSetting
    {
        [Key, Column(Order = 1)]
        public int ModuleId { get; set; }
        [Key, Column(Order = 2)]
        public int SettingId { get; set; }
        public string SettingValue { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
