using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Modules.Notification.Enums
{
    public enum NotificationSettingTypes
    {
        EmailNotificationEnabled = 0,
        SmsNotificationEnabled = 1,
        RealTimeNotificationEnabled = 2,
        PushNotificationEnabled = 3,

        SmsNotificationOverride = 4,
        SmsUsername = 5,
        SmsPassword = 6,
        SmsMethod = 7,
        SmsApiUrl = 8,
        SmsApiId = 9,
        SmsSender = 10,
        SmsSendId = 11,

        EmailCompressionEnabled = 12,
        EmailNotificationOverride = 13,
        EmailHost = 14,
        EmailPort = 15,
        EmailUsername = 16,
        EmailPassword = 17,
        EmailSslEnabled = 18,

        RealTimeAppId = 19,
        RealTimeAppKey = 20,
        RealTimeAppSecret = 21,
        RealTimePrefix = 22,

        PushApiToken = 23,
        PushApplicationToken = 24
    }
}
