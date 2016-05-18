using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientAffordabilityDetails")]
   public class ClientAffordabilityDetail
   {
       public Guid Id { get; set; }
       [Key]
       public long RefId { get; set; }
       public long ClientRefId { get; set; }
       public decimal GrossSalary { get; set; }
       public decimal Deductions { get; set; }
       public decimal NetSalary { get; set; }
       public decimal ExpensesHousing { get; set; }
       public decimal ExpensesTransport { get; set; }
       public decimal ExpensesConsumables { get; set; }
       public decimal ExpensesEducation { get; set; }
       public decimal ExpensesOther { get; set; }
       public decimal ExpensesOtherDebt { get; set; }
       public decimal ExpensesMedical { get; set; }
       public decimal ExpensesWater { get; set; }
       public decimal ExpensesMaintenance { get; set; }
       public DateTime DateCreated { get; set; }
       public DateTime DateModified { get; set; }
       public string UserCreated { get; set; }
       public string UserModified { get; set; }
    }
}
