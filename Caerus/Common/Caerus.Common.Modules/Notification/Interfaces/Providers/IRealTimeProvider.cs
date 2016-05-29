using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Notification.Interfaces.Providers
{
    public interface IRealTimeProvider
    {
        ReplyObject PushMessage<T>(string token, string eventName, T content);
    }
}
