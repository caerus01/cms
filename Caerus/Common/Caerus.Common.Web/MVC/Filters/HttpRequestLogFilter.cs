using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Caerus.Common.Enums;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Audit.ViewModels;
using Caerus.Common.Tools;
using Caerus.Modules.Audit.Service;

namespace Caerus.Common.Web.MVC.Filters
{
    public class HttpRequestLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var sourceIp = filterContext.HttpContext.Request.UserHostAddress;
            var urlParams = HttpTools.SerializeFormToString(filterContext.HttpContext.Request.QueryString);
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var contentType = filterContext.HttpContext.Request.ContentType;
            var body = "";
            try
            {
                if (filterContext.HttpContext.Request.InputStream != Stream.Null)
                    body = new StreamReader(filterContext.HttpContext.Request.InputStream, filterContext.HttpContext.Request.ContentEncoding).ReadToEnd();
            }
            catch (Exception ex)
            {
                new Logger().LogFatal(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                base.OnActionExecuting(filterContext);
                return;
            }

            var auditService = new HttpRequestLogService();
            auditService.LogRequest(new AuditRequestViewModel()
            {
                ContentType = contentType,
                Action = action,
                Controller = controller,
                RequestBody = body,
                UrlParams = urlParams,
                Source = sourceIp
            });
            base.OnActionExecuting(filterContext);
        }


    }
}
