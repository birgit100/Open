using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Common;
using Open.Core;

namespace Open.Tests.Data.Common
{
    [TestClass] public class IdentifiedDbRecordTests : ObjectsTests<IdentifiedDbRecord>
    {
        private class testClass : IdentifiedDbRecord { }

        protected override IdentifiedDbRecord getRandomTestObject()
        {
            return GetRandom.Object<testClass>();
        }

        [TestMethod]
        public void IsAbstract()
        {
            Assert.IsTrue(typeof(IdentifiedDbRecord).IsAbstract);
        }

        [TestMethod]
        public void BaseTypeIsUniqueDbRecord()
        {
           Assert.AreEqual(typeof(UniqueDbRecord), typeof(IdentifiedDbRecord).BaseType);
        }

        [TestMethod]
        public void IDTest()
        {
            testReadWriteProperty(() => obj.ID, x => obj.ID = x);
            testNullEmptyAndWhitespaceCases(() => obj.ID, x => obj.ID = x, () => obj.Name);
        }

        [TestMethod]
        public void NameTest()
        {
            testReadWriteProperty(() => obj.Name, x => obj.Name = x);
            testNullEmptyAndWhitespaceCases(() => obj.Name, x => obj.Name = x, () => obj.Code);
        }

        [TestMethod]
        public void CodeTest()
        {
            testReadWriteProperty(() => obj.Code, x => obj.Code = x);
            testNullEmptyAndWhitespaceCases(() => obj.Code, x => obj.Code = x, () => Constants.Unspecified);
        }

        private void testNullEmptyAndWhitespaceCases(Func<string> get, Func<string, string> set, Func<string> expected)
        {
            void test(string s)
            {
                set(s);
                Assert.AreEqual(get(), expected());
            }
            test(null);
            test(string.Empty);
            test("  ");
        }
    }
}
