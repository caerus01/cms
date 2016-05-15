using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Modules.Authentication.Entities;

namespace Caerus.Common.Modules.Authentication.Interfaces
{
    public interface IAuthenticationRepository : IRepository
    {
        void CreateUser(CaerusUser user);
    }
}
