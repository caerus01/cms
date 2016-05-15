using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Authentication.Entities
{
    [Table("AspNetUserRoles")]
    public class CaerusUserRole
    {
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        [Key, Column(Order = 2)]
        public string RoleId { get; set; }
    }
}
