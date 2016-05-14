using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Caerus.Common.Interfaces;
using Caerus.Common.Logging.Interfaces;

namespace Caerus.Common.Logging
{

    public class Logger : ICaerusLogger
    {
        public Logger()
        {
            Configure();
        }

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
            var source = ConfigurationManager.AppSettings["source"];
            log4net.LogicalThreadContext.Properties["Source"] = !string.IsNullOrEmpty(source) ? source : "unknown";
        }

        public void LogInfo(string infoMessage, dynamic[] properties = null)
        {
            try
            {
                var body = BuildLog(null, infoMessage, properties);
                log.Info(body.Message);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        // Logging a Debug message
        public void LogDebug(string debugMessage, dynamic[] properties = null)
        {
            try
            {
                var body = BuildLog(null, debugMessage, properties);
                log.Debug(body.Message);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        //Logging a Warning message
        public void LogWarning(string message, Exception exception = null, dynamic[] properties = null)
        {
            try
            {
                var body = BuildLog(exception, message, properties);
                log.Warn(body.Message, body.Exception);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        // Logging a Error message
        public void LogError(string methodName, Exception exception = null, dynamic[] properties = null)
        {
            try
            {
                var body = BuildLog(exception, methodName, properties);
                log.Error(body.Message, body.Exception);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        public void LogError(string error, dynamic[] properties = null)
        {
            try
            {
                var body = BuildLog(null, error, properties);
                log.Error(body.Message, null);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        public void LogError(Exception exc, dynamic[] properties = null)
        {
            try
            {
                var body = BuildLog(exc, "", properties);
                log.Error(body.Message, body.Exception);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        // Logging a Fatal message
        public void LogFatal(string methodName, Exception exception = null, dynamic[] properties = null)
        {


            try
            {
                var body = BuildLog(exception, "", properties);
                var message = body.Message;
                if (!String.IsNullOrEmpty(methodName))
                    message = methodName + " - " + message;

                log.Fatal(message, exception);
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        private void LoggerFail(Exception exception = null)
        {

            try
            {
                log4net.Config.XmlConfigurator.Configure();

                var body = BuildLog(exception);
                log.Fatal(body.Message, exception);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize logging", ex);
            }
        }

        public void LogSystemActivity(string description, long? reference = null, dynamic[] properties = null)
        {
            LogInfo(description + " : " + reference.ToString());
        }

        private LoggingBody BuildLog(Exception ex, string message = "", dynamic[] properties = null)
        {
            var body = new LoggingBody { Exception = ex };
            var frame = new StackTrace().GetFrame(2);
            body.LoggerMethod = frame.GetMethod().Name;

            var v = frame.GetMethod().DeclaringType;
            string assembly = "";
            if (v != null)
            {
                assembly = v.Assembly.GetName().Name;
            }
            log4net.LogicalThreadContext.Properties["Logger"] = string.Format("Source: {0} - Method: {1}", assembly, body.LoggerMethod);
            if (ex != null)
                body.Message = string.IsNullOrEmpty(message) ? string.Format("Exception has occurred in : {0}", body.LoggerMethod) : string.Format("Exception has occurred in : {0} - {1}", body.LoggerMethod, message);
            else
                body.Message = string.IsNullOrEmpty(message) ? string.Format("{0}", body.LoggerMethod) : string.Format("{0} - {1}", body.LoggerMethod, message);

            log4net.LogicalThreadContext.Properties["Parameters"] = "";
            log4net.LogicalThreadContext.Properties["User"] = "";
            log4net.LogicalThreadContext.Properties["Origin"] = "";
            if (properties != null && properties.Any())
            {
                var builder = new StringBuilder();
                var cnt = 0;
                foreach (var item in properties)
                {
                    cnt++;
                    builder.AppendLine(string.Format("({0})", item.ToString(), cnt));
                }
                log4net.LogicalThreadContext.Properties["Parameters"] = builder.ToString();
            }
            if (HttpContext.Current != null)
            {
                log4net.LogicalThreadContext.Properties["User"] = HttpContext.Current.User.Identity.Name;
                log4net.LogicalThreadContext.Properties["Origin"] = HttpContext.Current.Request.UserHostAddress;
            }
            return body;
        }


        private class LoggingBody
        {
            public string LoggerMethod { get; set; }

            public string Message { get; set; }

            public Exception Exception { get; set; }
        }

    }
}
