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

namespace SampleSite.Controllers
{
    public class FieldsController : ApiController
    {
        [HttpGet]
        public DynamicFieldReplyViewModel GetFields()
        {
            var session = new CaerusSession();
            var result = new DynamicFieldReplyViewModel();
            try
            {
                return session.FieldMappingService.GetEntityFieldsByRank(OwningTypes.Client, 0, 1);
            }
            catch (Exception ex)
            {
                result.ReplyStatus = ReplyStatus.Fatal;
                return result;
            }
        }
    }
}
