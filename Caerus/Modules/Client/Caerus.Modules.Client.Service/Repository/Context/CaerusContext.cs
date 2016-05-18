using System.Data.Entity;
using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;
using Caerus.Common.Modules.Audit.Entities;

namespace Caerus.Modules.Client.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override ModuleTypes ModuleId
        {
            get { return ModuleTypes.Client; }
        }

        public DbSet<Common.Modules.Client.Entities.Client> Clients { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientAddressDetail> ClientAddressDetails { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientAffordabilityDetail> ClientAffordabilityDetails { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientBankingDetail> ClientBankingDetails { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientBusiness> ClientBusinesses { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientContactDetail> ClientContactDetails { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientEmploymentDetail> ClientEmploymentDetails { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientIndivual> ClientIndivuals { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientNextOfKinDetail> ClientNextOfKinDetails { get; set; }
        public DbSet<Common.Modules.Client.Entities.ClientNote> ClientNotes { get; set; } 
    }
}
