using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;

namespace Open.Tests.Aids
{
    [TestClass]
    public class SystemRegionInfoTests : BaseTests
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(SystemRegionInfo);
        }

        [TestMethod]
        public void IsCountryTest()
        {
            Assert.IsFalse(SystemRegionInfo.IsCountry(null));
            //testEstonia();
            //testWorld();
        }

        [TestMethod]
        public void GetRegionsListTest()
        {
            Assert.Inconclusive();
        }

        private static void testEstonia()
        {
          
        }
    }
}
