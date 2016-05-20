using System;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Modules.Client.Service;
using Caerus.Modules.FieldMapping.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caerus.Tests.ServiceTests
{
    [TestClass]
    public class FieldMappingServiceTests : Base.BaseTest
    {
        public FieldMappingServiceTests() : base(true)
        {
            _session.ClientService = new ClientService(_session);
            _session.FieldMappingService = new FieldMappingService(_session);
        }
        [TestMethod]
        public void GetEntityFieldsByRank()
        {
            _session.FieldMappingService.GetEntityFieldsByRank(OwningTypes.Client, 0, FieldRanks.AccountRequirement);
        }


        [TestMethod]
        public void SaveEntityFields()
        {
            _session.FieldMappingService.SaveEntityFields(null);
        }
    }
}
