using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;

namespace Caerus.Common.ViewModels
{
    public class ReplyObject
    {
        public ReplyObject()
        {
            Errors = new List<string>();
        }
        public ReplyStatus ReplyStatus { get; set; }
        public ReplySubStatus ReplySubStatus { get; set; }
        public string ReplyMessage { get; set; }

        public List<string> Errors { get; set; } 
    }
}
