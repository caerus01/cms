using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Address.Interfaces;
using Caerus.Modules.Address.Service.Repository.Context;

namespace Caerus.Modules.Address.Service.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CaerusContext _context;
        public AddressRepository() 
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
