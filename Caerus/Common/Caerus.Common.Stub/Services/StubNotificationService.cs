using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Notification.Interfaces;
using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.Stub.Providers;
using Caerus.Common.Stub.Providers.Notification;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Services
{
    public class StubNotificationService : INotificationService
    {
        public ViewModels.ReplyObject SubmitNotification(Modules.Notification.Enums.DeliveryTypes deliveryType, Modules.Notification.Enums.NotificationTypes notificationType, string recipient, Dictionary<string, string> tokens = null, bool sendInProcess = false)
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public ViewModels.ReplyObject SubmitNotificationNoTemplate(Modules.Notification.Enums.DeliveryTypes devType, string recipient, string messageBody, string messageSubject = "", Dictionary<string, string> tokens = null, bool sendInProcess = false)
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public bool IsNotificationActive(Modules.Notification.Enums.DeliveryTypes deliveryType, Modules.Notification.Enums.NotificationTypes notificationType)
        {
            GlobalLogger.WrapStubInfo();
            return false;
        }

        public ViewModels.ReplyObject SaveNotificationTemplate()
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public ViewModels.ReplyObject GetNotificationTemplate()
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public ViewModels.ReplyObject SearchNotificationTemplates()
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public ISmsProvider SmsProvider
        {
            get { return new StubSmsProvider(); }
        }

        public IEmailProvider EmailProvider
        {
            get { return new StubEmailProvider(); }
        }

        public IPushProvider PushProvider
        {
            get { return new StubPushProvider(); }
        }

        public IRealTimeProvider RealTimeProvider
        {
            get { return new StubRealTimeProvider(); }
        }
    }
}
