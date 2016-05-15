using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Caerus.Authentication.Service.IdentityManagers;
using Caerus.Authentication.Service.Providers;
using Caerus.Authentication.Service.Repository;
using Caerus.Authentication.Service.Repository.Context;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Caerus.Authentication.Service
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
                _session.Logger.LogFatal(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, null);
                result.ReplyStatus = ReplyStatus.Fatal;
                result.ReplyMessage = string.Format("Exception was caught in {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

    }
}
