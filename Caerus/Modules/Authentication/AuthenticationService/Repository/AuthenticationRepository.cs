using Authentication.Service.Repository.Context;
using Caerus.Common.Modules.Authentication.Interfaces;

namespace Authentication.Service.Repository
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
