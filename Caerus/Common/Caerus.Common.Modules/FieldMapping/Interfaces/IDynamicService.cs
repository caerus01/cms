using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.FieldMapping.Interfaces
{
    public interface IDynamicService
    {
        OwningTypes OwningType { get; }
        List<DynamicEntityViewModel> GetEntityModelsByTypes(List<int> requiredEntityTypes, long owningEntityRef);
        ReplyObject SaveEntityFields(long owningEntityRef, List<DynamicResponseDataModel> entities);
    }
}
