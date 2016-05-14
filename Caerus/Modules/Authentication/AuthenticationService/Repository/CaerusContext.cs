using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationService.Repository
{
    public class CaerusContext : IdentityDbContext<CaerusUser>
    {
        public static CaerusContext Create()
        {
            return new CaerusContext();
        }
    }
}
