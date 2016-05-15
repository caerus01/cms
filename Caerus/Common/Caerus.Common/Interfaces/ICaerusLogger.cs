using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Interfaces
{
    public interface ICaerusLogger
    {
        void LogInfo(string infoMessage, dynamic[] properties = null);
        void LogDebug(string debugMessage, dynamic[] properties = null);
        void LogWarning(string Message, Exception exception = null, dynamic[] properties = null);
        void LogError(string methodName, Exception exception = null, dynamic[] properties = null);
        void LogError(string error, dynamic[] properties = null);
        void LogError(Exception exc, dynamic[] properties = null);
        void LogFatal(string methodName, Exception exception = null, dynamic[] properties = null);
        void LogSystemActivity(string description, long? reference = null, dynamic[] properties = null);
        ReplyObject WrapException(Exception ex, dynamic[] props = null, ReplyStatus replyStatus = ReplyStatus.Fatal);
    }
}
