using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Notification.Interfaces.Providers
{
   public interface IPushProvider
   {
       ReplyObject PushMessage(string deviceToken, string message, string subject);

       ReplyObject PushData(string deviceToken, string data, string content);
   }
}
