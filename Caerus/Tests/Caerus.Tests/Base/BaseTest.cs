using System;
using System.Web;
using System.Web.Mvc;
using Caerus.Common.Auth.Session;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

namespace Caerus.Tests.Base
{
    [TestClass]
    public class BaseTest
    {
        internal ICaerusSession _session;
        private string baseUser = "";
        internal MockController controller;
        public BaseTest(string userName = "")
        {
            _session = string.IsNullOrEmpty(userName) ? new CaerusSession() : new CaerusSession(userName);
            controller = new MockController();
            
        }

        public BaseTest(bool useRealSession)
        {
            _session = new CaerusSession(baseUser);
        }

        public BaseTest(ICaerusSession session)
        {
            _session = session;
        }

        public class MockController : Controller
        {

        }
    }
}
