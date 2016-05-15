using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.ViewModels;
using Caerus.Modules.Authentication.Service.IdentityManagers;
using Caerus.Modules.Authentication.Service.Providers;
using Caerus.Modules.Authentication.Service.Repository;
using Caerus.Modules.Authentication.Service.Repository.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Caerus.Modules.Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICaerusSession _session;
        private readonly IAuthenticationRepository _repository;

        private IOwinContext _context;
        internal int validateInterval = 30;
        internal int externalExpireTime = 5;
        internal string loginPath = "";
        public AuthenticationService(ICaerusSession session, IAuthenticationRepository repository = null, IOwinContext context = null)
        {
            _session = session;
            _repository = repository ?? new AuthenticationRepository(session);
            _context = context;
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

        public ReplyObject CreateNewUser(CaerusUser newUser)
        {
            var result = new ReplyObject();
            try
            {
                _repository.CreateUser(newUser);
            }
            catch (Exception ex)
            {
                result = _session.Logger.WrapException(ex);
            }
            return result;
        }

    }
}
