using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Notification.Enums;
using Caerus.Common.Modules.Notification.Interfaces.Providers;
using Caerus.Common.Modules.Notification.ViewModel;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.Tools;
using Caerus.Common.ViewModels;
using RestSharp;

namespace Caerus.Modules.Notification.Service.SmsProviders
{
    public class ClickatellSmsProvider : ISmsProvider
    {
        private ICaerusSession _session;
        private const int _maxMessageLength = 160;
        public ClickatellSmsProvider(ICaerusSession session)
        {
            _session = session;
        }
        public SmsReplyObject SendSms(string number, string message)
        {
            var returnObject = new SmsReplyObject() { SmsErrorType = NotificatonErrorTypes.NoError };
            message = Uri.EscapeUriString(message);
            var smsmsgs = message.Wrap(_maxMessageLength);
            foreach (var item in smsmsgs)
            {
                returnObject = SendClickatellSms(number, item);
                if (returnObject.SmsErrorType != NotificatonErrorTypes.NoError)
                    break;
                break;
            }
            return returnObject;
        }

        public SmsReplyObject SendClickatellSms(string number, string message)
        {
            var result = new SmsReplyObject();
            try
            {
                var userName = _session.ConfigurationService.GetModuleSetting<string>(ModuleTypes.Notification, (int)NotificationSettingTypes.SmsUsername);
                var password = _session.ConfigurationService.GetModuleSetting<string>(ModuleTypes.Notification, (int)NotificationSettingTypes.SmsPassword);
                var smsApiUrl = _session.ConfigurationService.GetModuleSetting<string>(ModuleTypes.Notification, (int)NotificationSettingTypes.SmsApiUrl);
                var apiId = _session.ConfigurationService.GetModuleSetting<string>(ModuleTypes.Notification, (int)NotificationSettingTypes.SmsApiId);
                var from = _session.ConfigurationService.GetModuleSetting<string>(ModuleTypes.Notification, (int)NotificationSettingTypes.SmsSendId);

                using (var request = new HttpTools.RestService(smsApiUrl, Method.GET))
                {
                    request.CurrentRequest.AddQueryParameter("user", userName);
                    request.CurrentRequest.AddQueryParameter("password", password);
                    request.CurrentRequest.AddQueryParameter("api_id", apiId);
                    request.CurrentRequest.AddQueryParameter("text", Uri.EscapeDataString(message));
                    if (!string.IsNullOrEmpty(from))
                    {
                        request.CurrentRequest.AddQueryParameter("mo", "1");
                        request.CurrentRequest.AddQueryParameter("from", from);
                    }
                    
                    var response = request.Execute("http/sendmsg");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.ReplyStatus = ReplyStatus.Fatal;
                        result.SmsErrorType = ProcessError(response.Content);
                        result.ReplyMessage = string.Format("Failed to send msg to {0} due to {1}", number, result.SmsErrorType.ToEnumerationDescription());
                        return result;
                    }

                    result.ReplyMessage =  response.Content.Replace("ID:", string.Empty).Trim();
                    result.ReplyStatus = ReplyStatus.Success;
                    result.SmsErrorType = NotificatonErrorTypes.NoError;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

        public SmsReplyObject ProcessReply(string number, string replyText, string reference)
        {
            return new SmsReplyObject();
        }

        private static NotificatonErrorTypes ProcessError(string returnValue)
        {

            switch (returnValue)
            {
                //Authentication details are incorrect.
                case "001": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //Authorization error, unknown user name or incorrect password.
                case "002": return NotificatonErrorTypes.LoginFailed;
                    break;
                //The session ID has expired after a pre-set time of inactivity.
                case "003": return NotificatonErrorTypes.NoCoverage;
                    break;
                //Missing session ID attribute in request.
                case "005": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //You have locked down the API instance to a specific IP address but attempted to send from an IP address different to the one you have set.
                case "007": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //The json/xml data submitted is invalid.
                case "100": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //One or more required parameters are missing or invalid.
                case "101": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The format of the user data header is incorrect.
                case "102": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The API message ID is unknown.
                case "103": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //The client ID message that you are querying does not exist.
                case "104": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //The destination address you are attempting to send to is invalid.
                case "105": return NotificatonErrorTypes.RecipientNumberInvalid;
                    break;
                //The sender address that is specified is incorrect.
                case "106": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //The message has no content
                case "107": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The API message ID is either incorrect or has not been included in the API call.
                case "108": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //Missing message ID. This can be either a client message ID or API message ID. For example when using the stop message command.
                case "109": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //The text component of the message is greater than the permitted 160 characters (70 Unicode characters). Select concat equal to 1,2,3-N to overcome this by splitting the content across multiple messages.
                case "113": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //This implies that the gateway is not currently routing messages to this network prefix. Please email support@clickatell.com with the mobile number in question.
                case "114": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //Message has expired before we were able to deliver it to the upstream gateway. No charge applies
                case "115": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //The format of the unicode data entered is incorrect.
                case "116": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The format of the delivery time entered is incorrect.
                case "120": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //This number is not allowed to receive messages from us and has been put on our block list
                case "121": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //The user has opted out and is no longer subscribed to your service.
                case "122": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //Invalid Sender ID. The Sender ID is not valid or has not been approved.
                case "123": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //Number delisted. This error may be returned when a number has been delisted.
                case "128": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //This error is returned when an account has exceeded the maximum number of MT messages which can be sent daily or monthly. You can send messages again on the date indicated by the UNIX TIMESTAMP.
                case "130": return NotificatonErrorTypes.ThirdPartyError;
                    break;
                //An unsupported HTTP method has been performed on the resource. Example: HTTP POST on the Coverage resource.
                case "160": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //You are attempting to access a Rest API resource that does not exist.
                case "161": return NotificatonErrorTypes.ConfigurationError;
                    break;
                //The expected header that specifies version was either not found or is invalid.
                case "165": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The optional header that specifies acceptable content does not contain an allowed value.
                case "166": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The expected header that specifies content-type content was either not found or did not contain an allowed value.
                case "167": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The batch ID which you have entered for batch messaging is not valid.
                case "201": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //The batch template has not been defined for the batch command.
                case "202": return NotificatonErrorTypes.MessageBodyError;
                    break;
                //Insufficient credits
                case "301": return NotificatonErrorTypes.InsufficientCredits;
                    break;
                //Please retry
                case "901": return NotificatonErrorTypes.TimeOut;
                    break;
                default: return NotificatonErrorTypes.MessageBodyError;
                    break;
            }


        }
    }
}
