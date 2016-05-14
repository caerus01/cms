using System;
using Caerus.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caerus.Tests.Session
{
    [TestClass]
    public class CaerusSessionTests : BaseTest
    {
        [TestMethod]
        public void LogFatal()
        {
            var parm1 = 1;
            var parm2 = "test";
            var parm3 = Guid.NewGuid();
            var parm4 = new { };
            try
            {
                throw new NotSupportedException("Test Exception");
            }
            catch (Exception ex)
            {
                _session.Logger.LogFatal("Test Method", ex, new dynamic[] { parm1, parm2, parm3, parm4 });
            }
        }
    }
}
