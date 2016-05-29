using Caerus.Common.Modules.Notification.ViewModel;

namespace Caerus.Common.Modules.Notification.Interfaces.Providers
{
    public interface ISmsProvider
    {
        SmsReplyObject SendSms(string number, string message);

        SmsReplyObject ProcessReply(string number, string replyText, string reference);
    }
}
