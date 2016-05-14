using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.IdentityUserManager;
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

       public override Task<ClaimsIdentity> CreateUserIdentityAsync(CaerusUser user)
        {
           return new Task<ClaimsIdentity>(() => null);
           //TODO: Implement User Claims Generation
           ///return user.GenerateUserIdentityAsync((CaerusUserManager)UserManager);
        }

       public static CaerusSignInManager Create(IdentityFactoryOptions<CaerusSignInManager> options, IOwinContext context)
        {
            return new CaerusSignInManager(context.GetUserManager<CaerusUserManager>(), context.Authentication);
        }
    }
}
