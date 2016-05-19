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
                            EntityObject = _repository.GetAddressDetailsByClientRefId(owningEntityRef) ?? new ClientAddressDetail() { ClientRefId = owningEntityRef },
                            EntityType = typeof(ClientAddressDetail)
                        };
                        break;
                    }
                case ClientEntityTypes.Affordability:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetAffordabilityDetailsByClientRefId(owningEntityRef) ?? new ClientAffordabilityDetail() { ClientRefId = owningEntityRef },
                            EntityType = typeof(ClientAffordabilityDetail)
                        };
                        break;
                    }
                case ClientEntityTypes.BankingDetail:
                    {
                        return new DynamicEntityViewModel()
                         {
                             OwningEntityType = type.AsInt(),
                             EntityObject = _repository.GetBankingDetailsByClientRefId(owningEntityRef) ?? new ClientBankingDetail() { ClientRefId = owningEntityRef },
                             EntityType = typeof(ClientBankingDetail)
                         };
                        break;
                    }
                case ClientEntityTypes.Business:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetBusinessesByClientRefId(owningEntityRef) ?? new ClientBusiness() { ClientRefId = owningEntityRef },
                            EntityType = typeof(ClientBusiness)
                        };
                        break;
                    }
                case ClientEntityTypes.Client:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetClient(owningEntityRef) ?? new Common.Modules.Client.Entities.Client(),
                            EntityType = typeof(Common.Modules.Client.Entities.Client)
                        };
                        break;
                    }
                case ClientEntityTypes.Contact:
                    {
                        return new DynamicEntityViewModel()
                         {
                             OwningEntityType = type.AsInt(),
                             EntityObject = _repository.GetContactDetailsByClientRefId(owningEntityRef) ?? new ClientContactDetail() { ClientRefId = owningEntityRef },
                             EntityType = typeof(ClientContactDetail)
                         };
                        break;
                    }
                case ClientEntityTypes.Employment:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetEmploymentDetailByClientRefId(owningEntityRef) ?? new ClientEmploymentDetail() { ClientRefId = owningEntityRef },
                            EntityType = typeof(ClientEmploymentDetail)
                        };
                        break;
                    }
                case ClientEntityTypes.Individual:
                    {
                        return new DynamicEntityViewModel()
                        {
                            OwningEntityType = type.AsInt(),
                            EntityObject = _repository.GetIndivualByClientRefId(owningEntityRef) ?? new ClientIndivual() { ClientRefId = owningEntityRef },
                            EntityType = typeof(ClientIndivual)
                        };
                        break;
                    }
                case ClientEntityTypes.NextOfKin:
                    {
                        return new DynamicEntityViewModel()
                         {
                             OwningEntityType = type.AsInt(),
                             EntityObject = _repository.GetNextOfKinDetailByClientRefId(owningEntityRef) ?? new ClientNextOfKinDetail() { ClientRefId = owningEntityRef },
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

        private ReplyObject SaveEntity<T>(ClientEntityTypes type, T data)
        {
            var result = new ReplyObject();
            try
            {

                switch (type)
                {
                    case ClientEntityTypes.Address:
                        {
                            _repository.UpsertAddressDetail(data as ClientAddressDetail);
                            break;
                        }
                    case ClientEntityTypes.Affordability:
                        {
                            _repository.UpsertAffordabilityDetail(data as ClientAffordabilityDetail);
                            break;
                        }
                    case ClientEntityTypes.BankingDetail:
                        {
                            _repository.UpsertBankingDetail(data as ClientBankingDetail);
                            break;
                        }
                    case ClientEntityTypes.Business:
                        {
                            _repository.UpsertBusiness(data as ClientBusiness);
                            break;
                        }
                    case ClientEntityTypes.Client:
                        {
                            _repository.UpsertClient(data as Common.Modules.Client.Entities.Client);
                            break;
                        }
                    case ClientEntityTypes.Contact:
                        {
                            _repository.UpsertContactDetail(data as ClientContactDetail);
                            break;
                        }
                    case ClientEntityTypes.Employment:
                        {
                            _repository.UpsertEmploymentDetail(data as ClientEmploymentDetail);
                            break;
                        }
                    case ClientEntityTypes.Individual:
                        {
                            _repository.UpsertIndivual(data as ClientIndivual);
                            break;
                        }
                    case ClientEntityTypes.NextOfKin:
                        {
                            _repository.UpsertNextOfKinDetail(data as ClientNextOfKinDetail);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
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

        public ReplyObject SaveEntityFields(long owningEntityRef, List<DynamicResponseDataModel> entities)
        {
            var result = new ReplyObject();
            try
            {
                foreach (var item in entities)
                {
                    var data = GetEntityModelByType((ClientEntityTypes)item.OwningEntityType, owningEntityRef);
                    if (data != null)
                    {
                        foreach (var fitem in item.Fields)
                        {
                            var prop = data.EntityType.GetProperty(fitem.Key);
                            if (prop != null)
                                prop.SetValue(data, fitem.Value);
                        }
                        SaveEntity((ClientEntityTypes) item.OwningEntityType, data);
                    }

                }
            }
            catch (Exception ex)
            {
                _session.Logger.WrapException(ex).CopyProperties(result);
            }
            return result;
        }

    
    }
}
