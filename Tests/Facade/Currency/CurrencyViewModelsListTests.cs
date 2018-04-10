using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Domain.Currency;
using Open.Facade.Currency;
using System.Collections.Generic;

namespace Open.Tests.Facade.Currency
{
    [TestClass]
    public class CurrencyViewModelsListTests : ObjectsTests<CurrencyViewModelsList>
    {
        protected override CurrencyViewModelsList getRandomTestObject()
        {
            var l = new List<CurrencyObject>();
            SetRandom.Values(l);
            return new CurrencyViewModelsList(l);
        }

        [TestMethod]
        public void CanCreateWithNullArgumentTest()
        {
            Assert.IsNotNull(new CurrencyViewModelsList(null));
        }
    }
}
