using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Notification.Enums
{
    public enum SmsErrorTypes
    {
        //no error
        NoError = 0,

        //sms error
        ThirdPartyError = 1,
        InsufficientCredits = 2,
        LoginFailed = 3,
        RecipientNumberInvalid = 4,
        TimeOut = 5,
        ConfigurationError = 6,
        MessageBodyError = 7,
        NoCoverage = 8,
    }
}
