using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Providers.Notification
{
    public class StubEmailProvider : IEmailProvider
    {
        public ReplyObject SendEmail(string address, string subject, string htmlbody, string fromAddress = "", string plainbody = "",
            byte[] attachments = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
