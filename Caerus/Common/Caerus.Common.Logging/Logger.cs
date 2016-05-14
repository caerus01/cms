using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        }

        public void LogInfo(string infoMessage, object[] properties = null)
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
        public void LogDebug(string debugMessage, object[] properties = null)
        {
            try
            {
                var body = BuildLog(null, debugMessage, properties);
                Task.Run(() => log.Debug(body.Message));
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        //Logging a Warning message
        public void LogWarning(string message, Exception exception = null, object[] properties = null)
        {
            try
            {
                var body = BuildLog(exception, message, properties);
                Task.Run(() => log.Warn(body.Message, body.Exception));
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        // Logging a Error message
        public void LogError(string methodName, Exception exception = null, object[] properties = null)
        {
            try
            {
                var body = BuildLog(exception, methodName, properties);
                Task.Run(() => log.Error(body.Message, body.Exception));
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        public void LogError(string error, object[] properties = null)
        {
            try
            {
                var body = BuildLog(null, error, properties);
                Task.Run(() => log.Error(body.Message, null));
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        public void LogError(Exception exc, object[] properties = null)
        {
            try
            {
                var body = BuildLog(exc, "", properties);
                Task.Run(() => log.Error(body.Message, body.Exception));
            }
            catch (Exception ex)
            {
                LoggerFail(ex);
            }
        }

        // Logging a Fatal message
        public void LogFatal(string methodName, Exception exception = null, object[] properties = null)
        {


            try
            {
                var body = BuildLog(exception, "", properties);
                var message = body.Message;
                if (!String.IsNullOrEmpty(methodName))
                    message = methodName + " - " + message;

                Task.Run(() => log.Fatal(message, exception));
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
                Task.Run(() => log.Fatal(body.Message, exception));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize logging", ex);
            }
        }

        public void LogSystemActivity(string description, long? reference = null, object[] properties = null)
        {
            LogInfo(description + " : " + reference.ToString());
        }

        private LoggingBody BuildLog(Exception ex, string message = "", object[] properties = null)
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
            body.Message += GetAdditionalMessages(ex);


            log4net.LogicalThreadContext.Properties["Parameters"] = "";
            if (properties != null && properties.Any())
            {
                var builder = new StringBuilder();
                var cnt = 0;
                foreach (var item in properties)
                {
                    cnt++;
                    builder.AppendLine(string.Format("{1} : {0}", item.ToString(), cnt));
                }
                log4net.LogicalThreadContext.Properties["Parameters"] = builder.ToString();
            }


            return body;
        }

        /// <summary>
        /// Gets additional error information based on handlers for specific exception types
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="depth">How deep to recurse into inner exceptions</param>
        /// <returns></returns>
        private string GetAdditionalMessages(Exception ex, int depth = 2)
        {
            try
            {
                var result = "";
                if (depth > 1 && ex.InnerException != null)
                {
                    result += "\n" + GetAdditionalMessages(ex.InnerException, depth - 1);
                }
                var types = GetLoggerHandlers();
                if (types == null || !types.Any()) { return result; }

                var handler = GetHandlerForCurrentMethod(ex, types);
                if (handler == null) { return result; }

                var method = handler.GetMethod("GetAdditionalMessage");
                if (method == null) { return result; }

                result += CallMessageHandlerMethod(ex, handler, method);

                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string CallMessageHandlerMethod(Exception ex, Type handler, MethodInfo method)
        {
            var instance = Activator.CreateInstance(handler);

            object[] parametersArray = new object[] { ex };
            var result = (string)method.Invoke(instance, parametersArray);
            return result;
        }

        private Type GetHandlerForCurrentMethod(Exception ex, IEnumerable<Type> types)
        {
            return types.FirstOrDefault(c =>
                c.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IExceptionLoggerHandler<>)
                    &&
                    x.GetGenericArguments().First() == ex.GetType()
                    )
                );
        }

        private IEnumerable<Type> GetLoggerHandlers()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == "Atlas.Common.Logging.LoggerTypeHandlers");
        }

        private class LoggingBody
        {
            public string LoggerMethod { get; set; }

            public string Message { get; set; }

            public Exception Exception { get; set; }
        }

    }
}
