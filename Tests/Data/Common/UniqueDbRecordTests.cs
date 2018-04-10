using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Common;

namespace Open.Tests.Data.Common
{
    [TestClass] public class UniqueDbRecordTests : ObjectsTests<UniqueDbRecord>
    {
        private class testClass : UniqueDbRecord { }

        protected override UniqueDbRecord getRandomTestObject()
        {
            return GetRandom.Object<testClass>();
        }

        [TestMethod]
        public void IsAbstract()
        {
            Assert.IsTrue(typeof(UniqueDbRecord).IsAbstract);
        }

        [TestMethod]
        public void BaseTypeIsUniqueDbRecord()
        {
            Assert.AreEqual(typeof(TemporalDbRecord), typeof(UniqueDbRecord).BaseType);
        }

        [TestMethod]
        public void IDTest()
        {
            testReadWriteProperty(() => obj.ID, x => obj.ID = x);
            testNullEmptyAndWhitespaceCases(() => obj.ID, x => obj.ID = x, () => obj.Name);
        }
    }
}
