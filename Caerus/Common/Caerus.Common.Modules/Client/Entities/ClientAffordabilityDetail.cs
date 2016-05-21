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
        public Nullable<long> ClientRefId { get; set; }
        public Nullable<decimal> GrossSalary { get; set; }
        public Nullable<decimal> Deductions { get; set; }
        public Nullable<decimal> NetSalary { get; set; }
        public Nullable<decimal> ExpensesHousing { get; set; }
        public Nullable<decimal> ExpensesTransport { get; set; }
        public Nullable<decimal> ExpensesConsumables { get; set; }
        public Nullable<decimal> ExpensesEducation { get; set; }
        public Nullable<decimal> ExpensesOther { get; set; }
        public Nullable<decimal> ExpensesOtherDebt { get; set; }
        public Nullable<decimal> ExpensesMedical { get; set; }
        public Nullable<decimal> ExpensesWater { get; set; }
        public Nullable<decimal> ExpensesMaintenance { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
