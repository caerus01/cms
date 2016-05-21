using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;

namespace Caerus.Common.Modules.FieldMapping.Interfaces
{
    public interface IFieldMappingRepository : IRepository
    {
        List<FieldDisplaySetup> GetEntityFieldsByRank(OwningTypes type, int rank);
        List<FieldDisplaySetup> GetEntityFieldsByView(OwningTypes type, int view);
        List<FieldDisplaySetup> GetEntityFieldsByEntityType(OwningTypes type, int entityType);
        List<FieldValidation> GetFieldValidationsByEntity(OwningTypes type, List<int> entities);

        List<FieldValidation> GetFieldValidationsByEntityAndField(OwningTypes type,
            List<FieldEntityViewModel> items);

    }
}
