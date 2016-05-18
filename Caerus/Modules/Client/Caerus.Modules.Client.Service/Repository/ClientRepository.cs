using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Modules.Client.Service.Repository.Context;

namespace Caerus.Modules.Client.Service.Repository
{
    public class ClientRepository : IClientRepository
    {
          private readonly CaerusContext _context;
          public ClientRepository()
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

     
    }
}
