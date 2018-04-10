using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Infra.Currency;

namespace Open.Tests.Infra.Currency
{
    [TestClass]
    public class CurrencyDbTableInitializerTests : BaseTests
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(CurrencyDbTableInitializer);
        }

        [TestMethod]
        public void InitializeTest()
        {
            Assert.Inconclusive();
        }
    }
}
