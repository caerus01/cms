using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.ViewModels;
using Caerus.Common.ViewModels;
using Owin;

namespace Caerus.Common.Modules.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        //ReplyObject RegisterUser(RegisterUserViewModel viewModel);
        ReplyObject CreateNewUser(CaerusUser newUser);
    }
}
