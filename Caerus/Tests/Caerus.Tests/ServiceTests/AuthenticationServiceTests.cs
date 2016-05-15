using System;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Caerus.Common.Modules.Authentication.Entities;
using Caerus.Common.Modules.Authentication.Enums;
using Caerus.Modules.Authentication.Service;
using Caerus.Tests.Base;
using Caerus.Tests.Fakes;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;


namespace Caerus.Tests.ServiceTests
{
    [TestClass]
    public class AuthenticationServiceTests : BaseTest
    {
        public AuthenticationServiceTests()
        {
            _session.AuthenticationService = new AuthenticationService(_session);
        }

        [TestMethod]
        public void CreatUser()
        {
            _session.AuthenticationService.CreateNewUser(new CaerusUser()
            {
                Email = "test2@test.com",
                UserName = "testing2",
                PhoneNumber = "0123311277",
                UserType = UserTypes.Api,
                PasswordHash = "123"
            });
        }

    }
}
