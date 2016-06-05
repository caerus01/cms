using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Notification.Enums;
using Caerus.Common.Modules.Notification.Interfaces;
using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.ViewModels;

namespace Caerus.Modules.Notification.Service
{
    public class NotificationService : INotificationService
    {
        public ReplyObject SubmitNotification(DeliveryTypes deliveryType, NotificationTypes notificationType, string recipient,
            Dictionary<string, string> tokens = null, bool sendInProcess = false)
        {
            return new ReplyObject();
        }

        public ReplyObject SubmitNotificationNoTemplate(DeliveryTypes deliveryType, string recipient, string messageBody,
            string messageSubject = "", Dictionary<string, string> tokens = null, bool sendInProcess = false)
        {
            return new ReplyObject();
        }

        public bool IsNotificationActive(DeliveryTypes deliveryType, NotificationTypes notificationType)
        {
            return false;
        }

        public ReplyObject SaveNotificationTemplate()
        {
            return new ReplyObject();
        }

        public ReplyObject GetNotificationTemplate(long refId)
        {
            return new ReplyObject();
        }

        public ReplyObject SearchNotificationTemplates(int step, int size, string orderBy, bool order, string filter)
        {
            return new ReplyObject();
        }

        public ISmsProvider SmsProvider { get; private set; }
        public IEmailProvider EmailProvider { get; private set; }
        public IPushProvider PushProvider { get; private set; }
        public IRealTimeProvider RealTimeProvider { get; private set; }
    }
}
