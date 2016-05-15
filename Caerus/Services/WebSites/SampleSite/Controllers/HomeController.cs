using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Caerus.Common.Auth.Session;
using Caerus.Common.Modules.Authentication.Enums;
using Caerus.Common.Modules.Authentication.ViewModels;
using Caerus.Common.Web;
using Caerus.Common.Web.MVC.Attributes;

namespace SampleSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var session = new CaerusSession();
            CaerusFormsAuthentication.SetAuthCookie(session, new UserViewModel(){Email = "test@test.com", PhoneNumber = "1234", UserName = "test", UserType = UserTypes.Internal, Id = "!23"});
            return View();
        }

        [CaerusAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CaerusAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}