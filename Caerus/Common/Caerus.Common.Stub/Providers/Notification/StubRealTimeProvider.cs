using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Providers.Notification
{
   public class StubRealTimeProvider : IRealTimeProvider
    {
       public ReplyObject PushMessage<T>(string token, string eventName, T content)
       {
           throw new System.NotImplementedException();
       }
    }
}
