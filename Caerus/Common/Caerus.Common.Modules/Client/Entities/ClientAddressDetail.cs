using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Client.Entities
{
    [Table("ClientAddressDetails")]
    public class ClientAddressDetail
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public long ClientRefId { get; set; }
        public string ResidentialAddressLine { get; set; }
        public string ResidentialSuburb { get; set; }
        public string ResidentialCity { get; set; }
        public long ResidentialProvinceRefId { get; set; }
        public string ResidentialZip { get; set; }
        public string ResidentialLongitude { get; set; }
        public string ResidentialLatitude { get; set; }
        public string PostalAddressLine { get; set; }
        public string PostalSuburb { get; set; }
        public string PostalCity { get; set; }
        public long PostalProvinceRefId { get; set; }
        public string PostalZip { get; set; }
        public string PostalLongitude { get; set; }
        public string PostalLatitude { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
