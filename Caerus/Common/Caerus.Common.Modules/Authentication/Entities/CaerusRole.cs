using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Authentication.Entities
{
    [Table("AspNetRoles")]
    public class CaerusRole
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
