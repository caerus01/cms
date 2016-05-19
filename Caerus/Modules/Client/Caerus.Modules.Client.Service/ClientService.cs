using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Client.Entities;
using Caerus.Common.Modules.Client.Enums;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.FieldMapping.Entities;
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

        private DynamicEntityViewModel GetEntityModelByType(ClientEntityTypes type, long owningEntityRef)
        {
            switch (type)
            {
                case ClientEntityTypes.Address:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetAddressDetailsByClientRefId(owningEntityRef),
                            EntityType = typeof(ClientAddressDetail)
                        };
                        break;
                    }
                case ClientEntityTypes.Affordability:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetAffordabilityDetailsByClientRefId(owningEntityRef),
                            EntityType = typeof(ClientAffordabilityDetail)
                        };
                        break;
                    }
                case ClientEntityTypes.BankingDetail:
                    {
                        return new DynamicEntityViewModel()
                         {
                             OwningEntityType = type.AsInt(),
                             EntityObject = _repository.GetBankingDetailsByClientRefId(owningEntityRef),
                             EntityType = typeof(ClientBankingDetail)
                         };
                        break;
                    }
                case ClientEntityTypes.Business:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetBusinessesByClientRefId(owningEntityRef),
                            EntityType = typeof(ClientBusiness)
                        };
                        break;
                    }
                case ClientEntityTypes.Client:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetClient(owningEntityRef),
                            EntityType = typeof(Common.Modules.Client.Entities.Client)
                        };
                        break;
                    }
                case ClientEntityTypes.Contact:
                    {
                        return new DynamicEntityViewModel()
                         {
                             OwningEntityType = type.AsInt(),
                             EntityObject = _repository.GetContactDetailsByClientRefId(owningEntityRef),
                             EntityType = typeof(ClientContactDetail)
                         };
                        break;
                    }
                case ClientEntityTypes.Employment:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetEmploymentDetailByClientRefId(owningEntityRef),
                            EntityType = typeof(ClientEmploymentDetail)
                        };
                        break;
                    }
                case ClientEntityTypes.Individual:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetIndivualByClientRefId(owningEntityRef),
                            EntityType = typeof(ClientIndivual)
                        };
                        break;
                    }
                case ClientEntityTypes.NextOfKin:
                    {
                        return new DynamicEntityViewModel()
                         {
                             OwningEntityType = type.AsInt(),
                             EntityObject = _repository.GetNextOfKinDetailByClientRefId(owningEntityRef),
                             EntityType = typeof(ClientNextOfKinDetail)
                         };
                        break;
                    }
                default:
                    {
                        return null;
                        break;
                    }
            }
        }

        public List<DynamicEntityViewModel> GetEntityModelsByTypes(List<int> requiredEntityTypes, long owningEntityRef)
        {
            var entities = new List<DynamicEntityViewModel>();
            foreach (var type in requiredEntityTypes)
            {
                var eType = (ClientEntityTypes)type;
                var entity = GetEntityModelByType(eType, owningEntityRef);
                if (entity != null)
                    entities.Add(entity);
            }
            return entities;
        }

        public ReplyObject SaveEntityFields(DynamicFieldReplyViewModel viewModel)
        {
            var result = new DynamicFieldReplyViewModel();
            try
            {

            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }


    }
}
