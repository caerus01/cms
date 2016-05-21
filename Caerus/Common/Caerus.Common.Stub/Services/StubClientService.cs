using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.FieldMapping.Entities;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Services
{
   public class StubClientService : IClientService
    {
       public OwningTypes OwningType {
           get
           {
               return OwningTypes.Client;
           }
       }

       public List<DynamicEntityViewModel> GetEntityModelsByTypes(List<int> requiredEntityTypes, long owningEntityRef)
       {
           GlobalLogger.WrapStubInfo();
           return new List<DynamicEntityViewModel>();
       }

       public ReplyObject SaveEntityFields(long owningEntityRef, List<DynamicResponseDataModel> entities)
       {
           GlobalLogger.WrapStubInfo();
           return new DynamicFieldReplyViewModel();
       }
    }
}
