using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Notification.Enums;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Notification.ViewModel
{
   public class SmsReplyObject : ReplyObject
    {
       public SmsErrorTypes SmsErrorType { get; set; }
    }
}
