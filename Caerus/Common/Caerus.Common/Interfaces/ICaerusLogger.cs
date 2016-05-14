using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Interfaces
{
    public interface ICaerusLogger
    {
        void LogInfo(string infoMessage, object[] properties = null);
        void LogDebug(string debugMessage, object[] properties = null);
        void LogWarning(string Message, Exception exception = null, object[] properties = null);
        void LogError(string methodName, Exception exception = null, object[] properties = null);
        void LogError(string error, object[] properties = null);
        void LogError(Exception exc, object[] properties = null);
        void LogFatal(string methodName, Exception exception = null, object[] properties = null);
        void LogSystemActivity(string description, long? reference = null, object[] properties = null);
    }
}
