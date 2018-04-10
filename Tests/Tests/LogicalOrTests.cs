using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Open.Tests.Tests
{
    [TestClass]
    public class LogicalOrTests
    {
        [TestMethod]
        public void BitwuseOrTest()
        {
            Assert.AreEqual(12, (int) (BindingFlags.Instance | BindingFlags.Static));
            Assert.AreEqual(12, 4 | 8);
            Assert.AreEqual(true, false | true);
            Assert.AreEqual(12u, 4u | 8u);
            Assert.AreEqual(108, 100 | 8 );      
        }

        [TestMethod]
        public void OrIsImpossibleTest()
        {
            bool or(bool x, bool y)
            {
                return x || y;
            }
            Assert.AreEqual(false, or(false, false));
            Assert.AreEqual(true, or(true, false));
        }
    }
}
