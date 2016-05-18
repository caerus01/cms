using Caerus.Modules.GeoCode.Service.Repository.Context;

namespace Caerus.Modules.GeoCode.Service.Repository
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
