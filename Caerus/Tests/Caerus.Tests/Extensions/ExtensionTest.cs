using System;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caerus.Tests.Extensions
{
    [TestClass]
    public class ExtensionTest
    {
        private class C1
        {
            public string Name { get; set; }
            public DateTime Age { get; set; }
            public ReplyStatus Status { get; set; }
        }

        private class C2
        {
            public string Name { get; set; }
            public Nullable<DateTime> Age { get; set; }
            public int Status { get; set; }
        }

        [TestMethod]
        public void TestCopyProperties()
        {
            
            var obj1 = new C1()
            {
               Name = "Jannie",
               Age = DateTime.Now,
               Status = ReplyStatus.Error
            };

            var obj2 = new C2()
            {

                Name = "Sannie",
                Age = null,
                Status = 0
            };

           obj1.CopyProperties(obj2);
           // obj2.CopyProperties(obj1);
        }
    }
}
