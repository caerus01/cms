using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Caerus.Common.Extentions;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Authentication.ViewModels;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.Tools;
using Newtonsoft.Json;

namespace Caerus.Common.Web
{
    public class CaerusFormsAuthentication
    {
        public static ClaimsIdentity GetCurrentUserPrincipal(FormsAuthenticationTicket ticket)
        {
            try
            {
                if (string.IsNullOrEmpty(ticket.UserData))
                    return null;

                var claimsIdentity = new ClaimsIdentity(new System.Security.Principal.GenericIdentity(ticket.Name, "Forms"));
                var data = ticket.UserData;
                var viewModel = JsonConvert.DeserializeObject<UserViewModel>(data);

                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, string.Format("UT{0}", viewModel.UserType.AsInt())));

                return claimsIdentity;
            }
            catch (Exception ex)
            {
                new Logger().LogFatal(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        public static void SetAuthCookie(ICaerusSession session, UserViewModel currentUser)
        {
            if (System.Web.HttpContext.Current == null)
                return;

            var data = JsonConvert.SerializeObject(currentUser);

            var ticket = new FormsAuthenticationTicket(1, currentUser.UserName, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, data, FormsAuthentication.FormsCookiePath);
            System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            if (System.Web.HttpContext.Current == null)
                return;
            foreach (var cookie in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpContext.Current.Request.Cookies.Remove(cookie);
            }
        }

        public static UserViewModel GetAuthUserDetails()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return null;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return null;

            return JsonConvert.DeserializeObject<UserViewModel>(authTicket.UserData);
        }
    }
}
