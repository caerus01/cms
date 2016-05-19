using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
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

        public List<FieldDisplaySetup> GetEntityFieldsByRank(OwningTypes type, FieldRanks rank)
        {
            return
                _context.FieldDisplaySetups.Where(c => c.OwningType == (int) type && c.FieldRank == (int) rank).ToList();
        }

        public List<FieldDisplaySetup> GetEntityFieldsByView(OwningTypes type, int view)
        {
            return
                _context.FieldDisplaySetups.Where(c => c.OwningType == (int)type && c.View == view).ToList();
        }

        public List<FieldDisplaySetup> GetEntityFieldsByEntityType(OwningTypes type, int entityType)
        {
            return
                _context.FieldDisplaySetups.Where(c => c.OwningType == (int)type && c.OwningEntityType == entityType).ToList();
        }
    }
}
