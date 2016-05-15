using Caerus.Authentication.Service.Repository.Context;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;

namespace Caerus.Authentication.Service.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private CaerusIdentityContext _identityContext;


        public AuthenticationRepository(ICaerusSession _session) 
        {
            _identityContext = CaerusIdentityContext.Create();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void CreateUser(CaerusUser user)
        {
            _identityContext.Users.Add(user);
            _identityContext.SaveChanges();
        }
    }
}
