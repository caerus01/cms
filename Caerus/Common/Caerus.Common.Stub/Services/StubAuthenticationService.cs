using Caerus.Common.Enums;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Services
{
    public class StubAuthenticationService : IAuthenticationService
    {
        public void ConfigureAuth(Owin.IAppBuilder app)
        {
            GlobalLogger.WrapStubInfo();
            return;
        }

        public ReplyObject CreateNewUser(CaerusUser newUser)
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }
    }
}
