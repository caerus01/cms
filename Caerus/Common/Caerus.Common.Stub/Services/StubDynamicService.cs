using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Services
{
   public class StubDynamicService : IDynamicService
    {
       public OwningTypes OwningType {
           get
           {
               return OwningTypes.Unknown;
           }
       }

       public List<DynamicEntityViewModel> GetEntityModelsByTypes(List<int> requiredEntityTypes, long owningEntityRef)
       {
           return new List<DynamicEntityViewModel>();
       }

       public ReplyObject SaveEntityFields(long owningEntityRef, List<DynamicResponseDataModel> entities)
       {
           return new ReplyObject();
       }
    }
}
