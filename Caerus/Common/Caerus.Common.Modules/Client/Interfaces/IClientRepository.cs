using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Data.Interfaces;
using Caerus.Common.Modules.Client.Entities;

namespace Caerus.Common.Modules.Client.Interfaces
{
    public interface IClientRepository : IRepository
    {
        void UpsertClient(Common.Modules.Client.Entities.Client item);
        void UpsertAddressDetail(ClientAddressDetail item);
        void UpsertAffordabilityDetail(ClientAffordabilityDetail item);
        void UpsertBankingDetail(ClientBankingDetail item);
        void UpsertBusiness(ClientBusiness item);
        void UpsertContactDetail(ClientContactDetail item);
        void UpsertEmploymentDetail(ClientEmploymentDetail item);
        void UpsertIndivual(ClientIndivual item);
        void UpsertNextOfKinDetail(ClientNextOfKinDetail item);
        Common.Modules.Client.Entities.Client GetClient(long clientRefId);
        ClientAddressDetail GetAddressDetailsByClientRefId(long clientRefId);
        ClientAffordabilityDetail GetAffordabilityDetailsByClientRefId(long clientRefId);
        ClientBankingDetail GetBankingDetailsByClientRefId(long clientRefId);
        ClientBusiness GetBusinessesByClientRefId(long clientRefId);
        ClientContactDetail GetContactDetailsByClientRefId(long clientRefId);
        ClientEmploymentDetail GetEmploymentDetailByClientRefId(long clientRefId);
        ClientIndivual GetIndivualByClientRefId(long clientRefId);
        ClientNextOfKinDetail GetNextOfKinDetailByClientRefId(long clientRefId);
    }
}
