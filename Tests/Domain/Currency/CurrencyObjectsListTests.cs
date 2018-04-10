using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Domain.Currency;

namespace Open.Tests.Domain.Currency
{
    [TestClass]
    public class CurrencyObjectsListTests : ObjectsTests<CurrencyObjectsList>
    {
        protected override CurrencyObjectsList getRandomTestObject()
        {
            return GetRandom.Object<CurrencyObjectsList>();
        }

        [TestMethod]
        public void CanCreateWithNullArgumentTest()
        {
            Assert.IsNotNull(new CurrencyObjectsList(null));
        }
    }
}
