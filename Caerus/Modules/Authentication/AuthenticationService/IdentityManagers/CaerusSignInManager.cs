using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Authentication.Service.IdentityManagers
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
