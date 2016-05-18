using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.ViewModels;
using Caerus.Modules.Client.Service.Repository;

namespace Caerus.Modules.Client.Service
{
    public class ClientService : IClientService
    {
        private readonly ICaerusSession _session;
        private readonly IClientRepository _repository;
        public ClientService(ICaerusSession session, IClientRepository repository = null)
        {
            _session = session;
            _repository = repository ?? new ClientRepository();
        }

        public OwningTypes OwningType
        {
            get
            {
                return OwningTypes.Client;
            }
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByRank(long owningEntityRef, FieldRanks fieldRank)
        {
            throw new NotImplementedException();
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByView(long owningEntityRef, int view)
        {
            throw new NotImplementedException();
        }

        public DynamicFieldReplyViewModel GetEntityFieldsByEntityType(long owningEntityRef, int entityType)
        {
            throw new NotImplementedException();
        }

        public ReplyObject SaveEntityFields(DynamicFieldReplyViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
