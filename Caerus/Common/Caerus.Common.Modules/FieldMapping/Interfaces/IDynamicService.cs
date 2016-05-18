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
    public interface IDynamicService
    {
        OwningTypes OwningType { get; }
        DynamicFieldReplyViewModel GetEntityFieldsByRank(long owningEntityRef, FieldRanks fieldRank);
        DynamicFieldReplyViewModel GetEntityFieldsByView(long owningEntityRef, int view);
        DynamicFieldReplyViewModel GetEntityFieldsByEntityType(long owningEntityRef, int entityType);
        ReplyObject SaveEntityFields(DynamicFieldReplyViewModel viewModel);
    }
}
