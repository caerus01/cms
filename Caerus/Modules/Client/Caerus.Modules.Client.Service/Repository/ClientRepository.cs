using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Client.Entities;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Modules.Client.Service.Repository.Context;

namespace Caerus.Modules.Client.Service.Repository
{


    public class ClientRepository : IClientRepository
    {
        private readonly CaerusContext _context;
        public ClientRepository()
        {
            _context = new CaerusContext();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }



        public void UpsertClient(Common.Modules.Client.Entities.Client item)
        {
            _context.Clients.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertAddressDetail(ClientAddressDetail item)
        {
            _context.ClientAddressDetails.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertAffordabilityDetail(ClientAffordabilityDetail item)
        {
            _context.ClientAffordabilityDetails.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertBankingDetail(ClientBankingDetail item)
        {
            _context.ClientBankingDetails.AddOrUpdate(item);
            _context.SaveChanges();
        }
        public void UpsertBusiness(ClientBusiness item)
        {
            _context.ClientBusinesses.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertContactDetail(ClientContactDetail item)
        {
            _context.ClientContactDetails.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertEmploymentDetail(ClientEmploymentDetail item)
        {
            _context.ClientEmploymentDetails.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertIndivual(ClientIndivual item)
        {
            _context.ClientIndivuals.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpsertNextOfKinDetail(ClientNextOfKinDetail item)
        {
            _context.ClientNextOfKinDetails.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public Common.Modules.Client.Entities.Client GetClient(long clientRefId)
        {
            return _context.Clients.Find(clientRefId);
        }

        public ClientAddressDetail GetAddressDetailsByClientRefId(long clientRefId)
        {
            return
                _context.ClientAddressDetails.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientAffordabilityDetail GetAffordabilityDetailsByClientRefId(long clientRefId)
        {
            return
                _context.ClientAffordabilityDetails.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientBankingDetail GetBankingDetailsByClientRefId(long clientRefId)
        {
            return
                _context.ClientBankingDetails.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientBusiness GetBusinessesByClientRefId(long clientRefId)
        {
            return
                _context.ClientBusinesses.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientContactDetail GetContactDetailsByClientRefId(long clientRefId)
        {
            return
                _context.ClientContactDetails.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientEmploymentDetail GetEmploymentDetailByClientRefId(long clientRefId)
        {
            return
                _context.ClientEmploymentDetails.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientIndivual GetIndivualByClientRefId(long clientRefId)
        {
            return
                _context.ClientIndivuals.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

        public ClientNextOfKinDetail GetNextOfKinDetailByClientRefId(long clientRefId)
        {
            return
                _context.ClientNextOfKinDetails.OrderByDescending(c => c.RefId)
                    .FirstOrDefault(c => c.ClientRefId == clientRefId);
        }

   
    }
}
