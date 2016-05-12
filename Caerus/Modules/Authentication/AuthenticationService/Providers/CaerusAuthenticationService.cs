using System;
using Orchard.Security;

namespace AuthenticationService.Providers
{
    public class CaerusAuthenticationService : IAuthenticationService
    {
        public void SignIn(IUser user, bool createPersistentCookie) {
            throw new NotImplementedException();
        }

        public void SignOut() {
            throw new NotImplementedException();
        }

        public void SetAuthenticatedUserForRequest(IUser user) {
            throw new NotImplementedException();
        }

        public IUser GetAuthenticatedUser() {
            throw new NotImplementedException();
        }
    }
}
