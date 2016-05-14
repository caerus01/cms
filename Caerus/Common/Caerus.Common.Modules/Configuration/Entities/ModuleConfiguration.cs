using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Configuration.Entities
{
    [Table("ModuleConfigurations")]
    public class ModuleConfiguration
    {
        [Key]
        public int ModuleId { get; set; }
        public int ServiceTypeId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    }
}
