using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Logging.Interfaces
{
    public interface IExceptionLoggerHandler<in T> where T : Exception
    {
        string GetAdditionalMessage(T exc);
    }

}
