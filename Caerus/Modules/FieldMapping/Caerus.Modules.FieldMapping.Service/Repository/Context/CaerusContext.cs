using System.Data.Entity;
using Caerus.Common.Data.DataProviders;
using Caerus.Common.Enums;
using Caerus.Common.Modules.FieldMapping.Entities;

namespace Caerus.Modules.FieldMapping.Service.Repository.Context
{
    public class CaerusContext : EfDataProvider
    {
        public override ModuleTypes ModuleId
        {
            get { return ModuleTypes.FieldMapping; }
        }

        public DbSet<FieldDisplaySetup> FieldDisplaySetups { get; set; }
        public DbSet<FieldValidation> FieldValidations { get; set; }
    }
}
