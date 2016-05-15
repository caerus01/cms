using System;
using System.Web;
using System.Web.Mvc;
using Caerus.Common.Auth.Session;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Tests.Fakes;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

namespace Caerus.Tests.Base
{
    [TestClass]
    public class BaseTest
    {
        internal ICaerusSession _session;
        private string baseUser = "";
        public BaseTest(string userName = "")
        {
            _session = string.IsNullOrEmpty(userName) ? new CaerusSession() : new CaerusSession(userName);
        }

        public BaseTest(bool useRealSession)
        {
            _session = new CaerusSession(baseUser);
        }

        public BaseTest(ICaerusSession session)
        {
            _session = session;
        }

        [TestInitialize]
        public void Init()
        {
            
        }

        public void Configuration(IAppBuilder app)
        {
            _session.AuthenticationService.ConfigureAuth(app);
        }
    }
}
