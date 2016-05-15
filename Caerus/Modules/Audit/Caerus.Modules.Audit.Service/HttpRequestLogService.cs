using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Audit.Entities;
using Caerus.Common.Modules.Audit.Interfaces;
using Caerus.Common.Modules.Audit.ViewModels;
using Caerus.Common.ViewModels;
using Caerus.Modules.Audit.Service.Repository;

namespace Caerus.Modules.Audit.Service
{
    public class HttpRequestLogService : IAuditService
    {
        protected readonly IAuditRepository _repository;
        public HttpRequestLogService(IAuditRepository repository = null)
        {
            _repository = repository ?? new AuditRepository();
        }


        public ReplyObject LogRequest(AuditRequestViewModel request)
        {
            var result = new ReplyObject();
            try
            {
                var log = new HttpRequestLog()
                {
                    Id = new Guid(),
                    RequestTime = DateTime.Now
                };

                request.CopyProperties(log);
                _repository.AddRequestLog(log);
            }
            catch (Exception ex)
            {
                new Logger().LogFatal(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                result.ReplyStatus = ReplyStatus.Fatal;
                result.ReplyMessage = string.Format("Exception was caught in {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
    }
}
