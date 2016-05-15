using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Audit.ViewModels
{
    public class AuditRequestViewModel : ReplyObject
    {
        public string Source { get; set; }
        public string ContentType { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string UrlParams { get; set; }
        public string RequestBody { get; set; }
    }
}
