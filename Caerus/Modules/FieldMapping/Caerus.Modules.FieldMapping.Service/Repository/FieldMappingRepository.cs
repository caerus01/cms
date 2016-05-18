using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Modules.FieldMapping.Service.Repository.Context;

namespace Caerus.Modules.FieldMapping.Service.Repository
{
    public class FieldMappingRepository : IFieldMappingRepository
    {
        private readonly CaerusContext _context;
        public FieldMappingRepository()
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
