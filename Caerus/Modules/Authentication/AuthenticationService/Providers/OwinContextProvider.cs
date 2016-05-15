using System.Runtime.Remoting.Messaging;
using Microsoft.Owin;

namespace Caerus.Authentication.Service.Providers
{
    public interface IOwinContextProvider
    {
        IOwinContext CurrentContext { get; }
    }
    public class OwinContextProvider : IOwinContextProvider
    {
        public IOwinContext CurrentContext
        {
            get { return (IOwinContext)CallContext.LogicalGetData("IOwinContext"); }
        }
    }
}
