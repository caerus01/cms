using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.Service.IdentityManagers;
using Authentication.Service.Providers;
using Authentication.Service.Repository.Context;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICaerusSession _session;
        private readonly IAuthenticationRepository _repository;
        internal int validateInterval = 30;
        internal int externalExpireTime = 5;
        internal string loginPath = "";
        public AuthenticationService(ICaerusSession session, IAuthenticationRepository repository = null)
        {
            _session = session;
            _repository = repository ?? null;
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(CaerusIdentityContext.Create);
            app.CreatePerOwinContext<CaerusUserManager>(CaerusUserManager.Create);
            app.CreatePerOwinContext<CaerusSignInManager>(CaerusSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(loginPath),
                Provider = new CaerausCookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<CaerusUserManager, CaerusUser>(TimeSpan.FromMinutes(validateInterval), GenerateUserIdentity)
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(externalExpireTime));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");
            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        public async Task<ClaimsIdentity> GenerateUserIdentity(UserManager<CaerusUser> manager, CaerusUser currentUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(currentUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
