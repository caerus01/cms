using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Audit.Entities
{
    [Table("HttpRequestLogs")]
    public class HttpRequestLog
    {
        public Guid Id { get; set; }
        [Key]
        public long RefId { get; set; }
        public string Source { get; set; }
        public DateTime RequestTime { get; set; }

        public string ContentType { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string UrlParams { get; set; }
        public string RequestBody { get; set; }
    }
}
