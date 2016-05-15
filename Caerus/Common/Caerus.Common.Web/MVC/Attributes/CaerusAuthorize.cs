using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Caerus.Common.Web.MVC.Attributes
{
    public class CaerusAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase actionContext)
        {
            if (!base.AuthorizeCore(actionContext))
                return false;
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket == null)
                    return false;

                var user = CaerusFormsAuthentication.GetCurrentUserPrincipal(authTicket);
                if (user == null)
                    return false;

                HttpContext.Current.User = new ClaimsPrincipal(user);
                return true;
            }
            return false;
        }



    }
}
