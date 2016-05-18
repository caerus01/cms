using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientBusinesses")]
   public class ClientBusiness
    {
        public Guid Id { get; set; }
       [Key]
        public long RefId { get; set; }
        public long ClientRefId { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public int BusinessType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
