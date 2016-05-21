using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientBankingDetails")]
   public class ClientBankingDetail
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public Nullable<long> ClientRefId { get; set; }
        public Nullable<long> BankRefId { get; set; }
        public Nullable<long> BankBranchRefId { get; set; }
        public string BankAccountNumber { get; set; }
        public Nullable<int> BankAccountType { get; set; }
        public string AccountHolder { get; set; }
        public Nullable<DateTime> CreditCardExiryDate { get; set; }
        public string CardReference { get; set; }
        public string CardCvv { get; set; }
        public string SecurityNumber { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<DateTime> LastActivityDate { get; set; }
        public string LastResponseCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
