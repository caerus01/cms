using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Notification.Enums;
using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Notification.Interfaces
{
    public interface INotificationService
    {
        ReplyObject SubmitNotification(DeliveryTypes deliveryType, NotificationTypes notificationType, string recipient, Dictionary<string, string> tokens = null, bool sendInProcess = false);

        ReplyObject SubmitNotificationNoTemplate(DeliveryTypes devType, string recipient, string messageBody, string messageSubject = "", Dictionary<string, string> tokens = null, bool sendInProcess = false);

        bool IsNotificationActive(DeliveryTypes deliveryType, NotificationTypes notificationType);

        ReplyObject SaveNotificationTemplate();

        ReplyObject GetNotificationTemplate();

        ReplyObject SearchNotificationTemplates();

        ISmsProvider SmsProvider { get; }
        IEmailProvider EmailProvider { get; }
        IPushProvider PushProvider { get; }
        IRealTimeProvider RealTimeProvider { get; }
    }
}
