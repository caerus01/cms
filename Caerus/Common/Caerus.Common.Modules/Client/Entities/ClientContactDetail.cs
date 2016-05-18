using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientContactDetails")]
   public class ClientContactDetail
    {
        public Guid Id { get; set; }
       [Key]
        public long RefId { get; set; }
        public long ClientRefId { get; set; }
        public string ContactPerson { get; set; }
        public string WorkTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string EmailAddress { get; set; }
        public string CellNumber { get; set; }
        public string WebSite { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string YouTube { get; set; }
        public string Skype { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
