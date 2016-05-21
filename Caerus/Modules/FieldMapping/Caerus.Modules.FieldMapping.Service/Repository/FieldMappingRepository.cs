using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.FieldMapping.ViewModels;
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

        public List<FieldDisplaySetup> GetEntityFieldsByRank(OwningTypes type, int rank)
        {
            return
                _context.FieldDisplaySetups.Where(c => c.OwningType == (int)type && c.FieldRank == rank).ToList();
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

        public List<FieldValidation> GetFieldValidationsByEntity(OwningTypes type, List<int> entities)
        {
            return
                _context.FieldValidations.Where(c => c.OwningType == (int)type && entities.Contains(c.OwningEntityType))
                    .ToList();
        }

        public List<FieldValidation> GetFieldValidationsByEntityAndField(OwningTypes type,
            List<FieldEntityViewModel> items)
        {
            var qry = from v in _context.FieldValidations
                      where v.OwningType == (int)type
                      select v;
            var types = items.Select(c => (int)c.OwningEntityType);
            qry = qry.Where(c => types.Contains(c.OwningEntityType));
            var list = qry.ToList();

            var result = new List<FieldValidation>();
            foreach (var item in items)
            {
                var val =
                    list.Where(c => c.OwningEntityType == item.OwningEntityType && c.FieldId == item.FieldId).ToList();
                if (val.Any())
                    result.AddRange(val);
            }
            return result;
        }
    }
}
