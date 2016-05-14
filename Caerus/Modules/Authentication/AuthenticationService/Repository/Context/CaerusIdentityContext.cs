using Caerus.Common.Data.DataProviders;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationService.Repository.Context
{
    public class CaerusIdentityContext : IdentityDbContext<CaerusUser>
    {
        public static CaerusIdentityContext Create()
        {
            return new CaerusIdentityContext();
        }

      
    }
}
