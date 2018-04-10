using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Open.Tests.Core
{
    [TestClass]
    public class RootObjectTests : ClassTests<RootObject>
    {
        private string x;
        private string y;
        private testClass obj;

        private class testClass : RootObject
        {
            public string S;
            public DateTime F;
            public DateTime T;
        }

        private void testGetValue(string field, string value, string expected)
        {
            obj.S = field;
            obj.getValue(ref obj.S, value);
            Assert.AreEqual(expected, obj.S);
        }

        private void testMinMax(Action method)
        {
            method();
            Assert.AreEqual(DateTime.MinValue, obj.F);
            Assert.AreEqual(DateTime.MaxValue, obj.T);
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new testClass { F = DateTime.MaxValue, T = DateTime.MinValue };
            x = GetRandom.String();
            y = GetRandom.String();
        }

        [TestMethod]
        public void GetValueTest()
        {
            testGetValue(null, y, y);
            testGetValue("", y, y);
            testGetValue("     ", y, y);
            testGetValue(x, y, x);
        }

        [TestMethod]
        public void GetMinValueTest()
        {
            testMinMax(() => obj.getMinValue(ref obj.F, ref obj.T));
        }

        [TestMethod]
        public void GetMaxValueTest()
        {
            testMinMax(() => obj.getMaxValue(ref obj.T, ref obj.F));
        }
    }
}
