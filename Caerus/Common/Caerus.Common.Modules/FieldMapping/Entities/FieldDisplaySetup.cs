using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.FieldMapping.Entities
{
    [Table("FieldDisplaySetups")]
    public class FieldDisplaySetup
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public int OwningType { get; set; }
        public int OwningEntityType { get; set; }
        public int View { get; set; }
        public int Sequence { get; set; }
        public string FieldId { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
        public int FieldType { get; set; }
        public string CssClass { get; set; }
        public int FieldRank { get; set; }
        public int LookupType { get; set; }
        public int ReadOnly { get; set; }
        public bool ExcludeApi { get; set; }
        public bool ExcludeInternal { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string FieldMask { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
