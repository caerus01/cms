using Caerus.Common.Modules.Contact.Interfaces;
using Caerus.Modules.Contact.Service.Repository.Context;

namespace Caerus.Modules.Contact.Service.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly CaerusContext _context;
        public ContactRepository() 
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
