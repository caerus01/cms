using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.FieldMapping.Interfaces
{
    public interface IFieldMappingService
    {
        DynamicFieldReplyViewModel GetEntityFieldsByRank(OwningTypes entityType, long owningEntityRef, int fieldRank);
        DynamicFieldReplyViewModel GetEntityFieldsByView(OwningTypes entityType, long owningEntityRef, int view);
        DynamicFieldReplyViewModel GetEntityFieldsByEntityType(OwningTypes entityType, long owningEntityRef, int owningEntityType);
        ReplyObject SaveEntityFields(DynamicFieldReplyViewModel viewModel);
        bool IsValid(FieldValidationTypes type, string value, string validationValue);
        ReplyObject AssignFields(Dictionary<string, dynamic> fields, dynamic entity, Type entityType);

        NextOutstandingCheckViewModel GetNextOutstandingEntityByRank(OwningTypes entityType, long owningEntityRef, int maxRankCheck = 2);
          
    }
}
