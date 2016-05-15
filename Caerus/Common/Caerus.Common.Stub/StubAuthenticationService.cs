using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub
{
    public class StubAuthenticationService : IAuthenticationService
    {
        public void ConfigureAuth(Owin.IAppBuilder app)
        {
            return;
        }

        public ReplyObject CreateNewUser(CaerusUser newUser)
        {
            return new ReplyObject()
            {
                ReplyStatus = ReplyStatus.Warning,
                ReplyMessage = "Not in use"
            };
        }
    }
}
