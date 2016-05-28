using Caerus.Common.Modules.Lookup.Interfaces;
using Caerus.Modules.Lookup.Service.Repository.Context;

namespace Caerus.Modules.Lookup.Service.Repository
{
    public class LookupRepository : ILookupRepository
    {
        private readonly CaerusContext _context;
        public LookupRepository()
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
