using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Logging;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Stub.Services
{
    public class StubFieldMappingService : IFieldMappingService
    {
        public DynamicFieldReplyViewModel GetEntityFieldsByRank(OwningTypes entityType, long owningEntityRef, int fieldRank)
        {

            GlobalLogger.WrapStubInfo();
            return new DynamicFieldReplyViewModel();
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByView(Modules.FieldMapping.Enums.OwningTypes entityType, long owningEntityRef, int view)
        {
            GlobalLogger.WrapStubInfo();
            return new DynamicFieldReplyViewModel();
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByEntityType(Modules.FieldMapping.Enums.OwningTypes entityType, long owningEntityRef, int owningEntityType)
        {
            GlobalLogger.WrapStubInfo();
            return new DynamicFieldReplyViewModel();
        }

        public ReplyObject SaveEntityFields(DynamicFieldReplyViewModel viewModel)
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public bool IsValid(Modules.FieldMapping.Enums.FieldValidationTypes type, string value, string validationValue)
        {
            GlobalLogger.WrapStubInfo();
            return true;
        }

        public ReplyObject AssignFields(Dictionary<string, dynamic> fields, dynamic entity, Type entityType)
        {
            GlobalLogger.WrapStubInfo();
            return new ReplyObject();
        }

        public NextOutstandingCheckViewModel GetNextOutstandingEntityByRank(OwningTypes entityType, long owningEntityRef,
            int maxRankCheck = 2)
        {
            GlobalLogger.WrapStubInfo();
            return new NextOutstandingCheckViewModel();
        }
    }
}
