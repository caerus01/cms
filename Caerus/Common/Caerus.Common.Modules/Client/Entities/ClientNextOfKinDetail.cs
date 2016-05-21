using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientNextOfKinDetails")]
   public class ClientNextOfKinDetail
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public Nullable<long> ClientRefId { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string Surname { get; set; }
        public Nullable<int> GenderType { get; set; }
        public Nullable<int> Title { get; set; }
        public Nullable<int> Relationship { get; set; }
        public Nullable<int> Nationality { get; set; }
        public Nullable<int> TypeOfId { get; set; }
        public string IdNumber { get; set; }
        public string HomeTelephone { get; set; }
        public string WorkTelephone { get; set; }
        public string CellNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
