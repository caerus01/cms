using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.FieldMapping.Entities
{
    [Table("FieldValidations")]
   public class FieldValidation
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public int OwningType { get; set; }
        public int OwningEntityType { get; set; }
        public int View { get; set; }
        public string FieldId { get; set; }
        public int ValidationType { get; set; }
        public string ValidationValue { get; set; }
        public string ValidationMessage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
