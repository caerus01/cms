using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.Modules.Notification.ViewModel;

namespace Caerus.Common.Stub.Providers.Notification
{
    public class StubSmsProvider : ISmsProvider
    {
        public SmsReplyObject SendSms(string number, string message)
        {
            throw new System.NotImplementedException();
        }

        public SmsReplyObject ProcessReply(string number, string replyText, string reference)
        {
            throw new System.NotImplementedException();
        }
    }
}
