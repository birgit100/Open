using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Facade.Currency;

namespace Open.Tests.Facade.Currency
{
    [TestClass]
    public class CurrencyViewModelTests : ObjectsTests<CurrencyViewModel>
    {
        [TestMethod]
        public new void CanCreateTest()
        {
            Assert.IsNotNull(new CurrencyViewModel());
        }

        protected override CurrencyViewModel getRandomTestObject()
        {
            return GetRandom.Object<CurrencyViewModel>();
        }

        [TestMethod]
        public void NameTest()
        {
            testReadWriteProperty(() => obj.Name, x => obj.Name = x);
        }

        [TestMethod]
        public void ValidFromTest()
        {
            DateTime? rnd() => GetRandom.DateTime(null, obj.ValidFrom?.AddYears(-1));
            testReadWriteProperty(() => obj.ValidFrom, x => obj.ValidFrom = x, rnd);
        }

        [TestMethod]
        public void ValidToTest()
        {
            DateTime? rnd() => GetRandom.DateTime(obj.ValidFrom?.AddYears(1));
            testReadWriteProperty(() => obj.ValidTo, x => obj.ValidTo = x, rnd);
        }

        [TestMethod]
        public void Alpha3CodeTest()
        {
            testReadWriteProperty(() => obj.Alpha3Code, x => obj.Alpha3Code = x);
        }

        [TestMethod]
        public void CurrencySymbolTest()
        {
            testReadWriteProperty(() => obj.CurrencySymbol, x => obj.CurrencySymbol = x);
        }
    }
}
