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
            throw new NotImplementedException();
        }

        public ReplyObject SubmitNotificationNoTemplate(DeliveryTypes deliveryType, string recipient, string messageBody,
            string messageSubject = "", Dictionary<string, string> tokens = null, bool sendInProcess = false)
        {
            throw new NotImplementedException();
        }

        public bool IsNotificationActive(DeliveryTypes deliveryType, NotificationTypes notificationType)
        {
            throw new NotImplementedException();
        }

        public ReplyObject SaveNotificationTemplate()
        {
            throw new NotImplementedException();
        }

        public ReplyObject GetNotificationTemplate()
        {
            throw new NotImplementedException();
        }

        public ReplyObject SearchNotificationTemplates()
        {
            throw new NotImplementedException();
        }

        public ISmsProvider SmsProvider { get; private set; }
        public IEmailProvider EmailProvider { get; private set; }
        public IPushProvider PushProvider { get; private set; }
        public IRealTimeProvider RealTimeProvider { get; private set; }
    }
}
