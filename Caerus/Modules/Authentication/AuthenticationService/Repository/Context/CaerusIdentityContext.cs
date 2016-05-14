using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Authentication.Service.Repository.Context
{
    public class CaerusIdentityContext : IdentityDbContext<CaerusUser>
    {
        public static CaerusIdentityContext Create()
        {
            return new CaerusIdentityContext();
        }

      
    }
}
