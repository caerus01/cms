using System;

namespace AuthenticationService.Providers
{
    public class CaerusMemberShipService : Orchard.Security.IMembershipService
    {
        public Orchard.Security.MembershipSettings GetSettings()
        {
            throw new NotImplementedException();
        }

        public Orchard.Security.IUser CreateUser(Orchard.Security.CreateUserParams createUserParams)
        {
            throw new NotImplementedException();
        }

        public Orchard.Security.IUser GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public Orchard.Security.IUser ValidateUser(string userNameOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public void SetPassword(Orchard.Security.IUser user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
