using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Caerus.Common.Auth.Session;
using Caerus.Common.Modules.Authentication.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SampleSite.Models;

namespace SampleSite.Controllers
{
   
    public class AccountController : Controller
    {

        public AccountController()
        {
           
        }

        public void Register()
        {
            var session =new CaerusSession();
            session.AuthenticationService.CreateNewUser(new CaerusUser()
            {
                Email = "test@test.com",
                UserName = "testing",
                PhoneNumber = "0123311275",

            });
        }
    }
}