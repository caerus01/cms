using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("Clients")]
    public class Client
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public string ShortDescription { get; set; }
        public int ClientType { get; set; }
        public int ClientStatus { get; set; }
        public string OriginSourceIp { get; set; }
        public string ExternalReference { get; set; }
        public string MainUserAccountId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
