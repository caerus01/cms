using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;

namespace AuthenticationService.Providers
{
    public class CaerausCookieAuthenticationProvider : CookieAuthenticationProvider
    {
        public override void ResponseSignOut(CookieResponseSignOutContext context)
        {
            base.ResponseSignOut(context);
        }

        public override void ResponseSignedIn(CookieResponseSignedInContext context)
        {
            base.ResponseSignedIn(context);
        }

        public override void ApplyRedirect(CookieApplyRedirectContext context)
        {
            base.ApplyRedirect(context);
        }

        public override Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return base.ValidateIdentity(context);
        }

        public override void Exception(CookieExceptionContext context)
        {
            base.Exception(context);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
