using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientIndivuals")]
   public class ClientIndivual
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string PassportNumber { get; set; }
        public Nullable<long> ClientRefId { get; set; }
        public string Initials { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public Nullable<int> GenderType { get; set; }
        public Nullable<int> MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public Nullable<long> TitleRefId { get; set; }
        public Nullable<long> LanguageRefId { get; set; }
        public Nullable<int> EducationLevel { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
