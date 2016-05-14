using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Repository.Context;
using Caerus.Common.Modules.Authentication.Interfaces;

namespace AuthenticationService.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {

        private CaerusIdentityContext _identityContext;
        public CaerusIdentityContext GetIdentityContext()
        {
            return _identityContext ?? (_identityContext = CaerusIdentityContext.Create());
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
