using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Providers.Notification
{
    public class StubPushProvider : IPushProvider
    {
        public ReplyObject PushMessage(string deviceToken, string message, string subject)
        {
            throw new System.NotImplementedException();
        }

        public ReplyObject PushData(string deviceToken, string data, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}
