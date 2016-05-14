using System;
using Authentication.Service;
using Caerus.Tests.Base;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
        public void ConfigureAuth()
        {
            WebApp.Start<AuthenticationServiceTests>("http://localhost:12345");
        }
    }
}
