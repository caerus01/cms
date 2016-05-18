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
        public long ClientRefId { get; set; }
        public string Initials { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderType { get; set; }
        public int MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public long TitleRefId { get; set; }
        public long LanguageRefId { get; set; }
        public int EducationLevel { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
