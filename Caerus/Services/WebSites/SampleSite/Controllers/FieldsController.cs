using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Caerus.Common.Auth.Session;
using Caerus.Common.Enums;
using Caerus.Common.Modules.FieldMapping.Enums;
using Caerus.Common.Modules.FieldMapping.ViewModels;
using Caerus.Common.ViewModels;

namespace SampleSite.Controllers
{
    public class FieldsController : ApiController
    {
        [HttpGet]
        public DynamicFieldReplyViewModel GetFields(long clientRefId)
        {
            var session = new CaerusSession();
            var result = new DynamicFieldReplyViewModel();
            try
            {
                return session.FieldMappingService.GetEntityFieldsByRank(OwningTypes.Client, clientRefId, 1);
            }
            catch (Exception ex)
            {
                result.ReplyStatus = ReplyStatus.Fatal;
                return result;
            }
        }


        [HttpPost]
        public ReplyObject SaveFields(DynamicFieldReplyViewModel model)
        {
            var session = new CaerusSession();
            var result = new ReplyObject();
            try
            {
                model.OwningType = OwningTypes.Client;
                return session.FieldMappingService.SaveEntityFields(model);
            }
            catch (Exception ex)
            {
                result.ReplyStatus = ReplyStatus.Fatal;
                return result;
            }
        }
    }
}
