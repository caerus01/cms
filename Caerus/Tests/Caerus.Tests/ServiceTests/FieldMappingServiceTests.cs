using System;
using System.Collections.Generic;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;
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
            _session.FieldMappingService.SaveEntityFields(new DynamicFieldReplyViewModel()
            {
                OwningEntityRef = 1,
                OwningType = OwningTypes.Client,
                Fields = new List<FieldItemModel>()
                {
                    new FieldItemModel()
                    {
                        FieldId = "FirstName",
                        OwningEntityType = 1,
                        FieldValue = "Test"
                    },
                    new FieldItemModel()
                    {
                        FieldId = "BankAccountNumber",
                        OwningEntityType = 3,
                        FieldValue = "123"
                    },
                }
            });
        }
    }
}
