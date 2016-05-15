using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Caerus.Authentication.Service.Repository.Context
{
    public class CaerusIdentityContext : IdentityDbContext<CaerusUser>
    {
        public CaerusIdentityContext()
            : base("CaerusContext", throwIfV1Schema: false)
        {
            
        }
        public static CaerusIdentityContext Create()
        {
            return  new CaerusIdentityContext();
        }

      
    }
}
