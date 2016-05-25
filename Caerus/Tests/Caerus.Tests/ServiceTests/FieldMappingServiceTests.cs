using System;
using System.Collections.Generic;
using Caerus.Common.Modules.Client.Enums;
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
        public FieldMappingServiceTests()
            : base(true)
        {
            _session.ClientService = new ClientService(_session);
            _session.FieldMappingService = new FieldMappingService(_session);
        }
        [TestMethod]
        public void GetEntityFieldsByRank()
        {
            _session.FieldMappingService.GetEntityFieldsByRank(OwningTypes.Client, 0, (int)ClientEntityRanks.AccountRequirement);
        }


        [TestMethod]
        public void SaveEntityFields()
        {
            _session.FieldMappingService.SaveEntityFields(new DynamicFieldReplyViewModel()
            {
                OwningEntityRef = 2,
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
                        FieldId = "DateOfBirth",
                        OwningEntityType = 1,
                        FieldValue = DateTime.Now
                    },
                    new FieldItemModel()
                    {
                        FieldId = "BankAccountNumber",
                        OwningEntityType = 3,
                        FieldValue = "123"
                    }
                    ,
                    new FieldItemModel()
                    {
                        FieldId = "LastActivityDate",
                        OwningEntityType = 3,
                        FieldValue =new DateTime(0001, 01, 01)
                    },
                    new FieldItemModel()
                    {
                        FieldId = "PostalAddressLine",
                        OwningEntityType = 4,
                        FieldValue = "1170 Newman Street"
                    },
                    new FieldItemModel()
                    {
                        FieldId = "ResidentialCity",
                        OwningEntityType = 4,
                        FieldValue = "Pretoria"
                    },
                    new FieldItemModel()
                    {
                        FieldId = "EmailAddress",
                        OwningEntityType = 5,
                        FieldValue = "test@123.com"
                    }
                }
            });
        }


        [TestMethod]
        public void GetNextOutstandingEntityByRank()
        {
          var result1 =   _session.FieldMappingService.GetNextOutstandingEntityByRank(OwningTypes.Client, 1, (int)ClientEntityRanks.AccountRequirement);
          var result2 = _session.FieldMappingService.GetNextOutstandingEntityByRank(OwningTypes.Client, 2, (int)ClientEntityRanks.AccountRequirement);

           result1 = _session.FieldMappingService.GetNextOutstandingEntityByRank(OwningTypes.Client, 1, (int)ClientEntityRanks.ProductRequirement);
           result2 = _session.FieldMappingService.GetNextOutstandingEntityByRank(OwningTypes.Client, 2, (int)ClientEntityRanks.ProductRequirement);
        }
    }
}
