using System;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Modules.Authentication.Service.Repository.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Caerus.Modules.Authentication.Service.IdentityManagers
{
    public class CaerusUserManager : UserManager<CaerusUser>
    {

        internal string tokenName = "Caerus Token";
        internal int passwordLength = 6;
        internal bool requireNonLetterOrDigit = true;
        internal bool requireDigit = true;
        internal bool requireLowercase = true;
        internal bool requireUppercase = true;
        internal bool userLockout = true;
        internal bool requireUniqueEmail = true;
        internal bool allowAlphaNumericUserNames = false;
        internal int maxAccessAttempts = 5;
        internal int lockoutTimespan = 5;
        public CaerusUserManager(IUserStore<CaerusUser> store)
            : base(store)
        {
        }


        public static CaerusUserManager Create(IdentityFactoryOptions<CaerusUserManager> options, IOwinContext context)
        {
            var manager = new CaerusUserManager(new UserStore<CaerusUser>(context.Get<CaerusIdentityContext>()));

            manager.UserValidator = new UserValidator<CaerusUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = manager.allowAlphaNumericUserNames,
                RequireUniqueEmail = manager.requireUniqueEmail
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = manager.passwordLength,
                RequireNonLetterOrDigit = manager.requireNonLetterOrDigit,
                RequireDigit = manager.requireDigit,
                RequireLowercase = manager.requireLowercase,
                RequireUppercase = manager.requireUppercase,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = manager.userLockout;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(manager.lockoutTimespan);
            manager.MaxFailedAccessAttemptsBeforeLockout = manager.maxAccessAttempts;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<CaerusUser>(dataProtectionProvider.Create(manager.tokenName));
            }
           
            return manager;
        }
    }
}
