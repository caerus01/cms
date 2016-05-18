using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientEmploymentDetails")]
   public class ClientEmploymentDetail
    {
        public Guid Id { get; set; }
       [Key]
        public long RefId { get; set; }
        public long ClientRefId { get; set; }
        public int EmployerSectorId { get; set; }
        public int EmploymentLevel { get; set; }
        public int EmploymentType { get; set; }
        public DateTime EmploymentStarted { get; set; }
        public int SalaryRule { get; set; }
        public int SalaryDayRule { get; set; }
        public string PayslipUsername { get; set; }
        public string PayslipPassword { get; set; }
        public string EmployerContactPerson { get; set; }
        public string EmployerContactNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
