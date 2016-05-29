using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Notification.Interfaces.Providers
{
    public interface IEmailProvider
    {
        ReplyObject SendEmail(string address, string subject, string htmlbody, string fromAddress = "", string plainbody = "", byte[] attachments = null);
    }
}
