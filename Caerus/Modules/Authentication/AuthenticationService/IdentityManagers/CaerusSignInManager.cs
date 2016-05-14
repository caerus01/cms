using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace AuthenticationService.IdentityManagers
{
   public class CaerusSignInManager: SignInManager<CaerusUser, string>
    {
       public CaerusSignInManager(CaerusUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

       public static CaerusSignInManager Create(IdentityFactoryOptions<CaerusSignInManager> options, IOwinContext context)
        {
            return new CaerusSignInManager(context.GetUserManager<CaerusUserManager>(), context.Authentication);
        }
    }
}
