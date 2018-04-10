using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Core;
using Open.Domain.Currency;
using Open.Facade.Currency;
using System;

namespace Open.Tests.Facade.Currency
{
    [TestClass]
    public class CurrencyViewModelFactoryTests : BaseTests
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(CurrencyViewModelFactory);
        }

        [TestMethod]
        public void CreateTest()
        {
            var o = GetRandom.Object<CurrencyObject>();
            var v = CurrencyViewModelFactory.Create(o);
            Assert.AreEqual(v.Name, o.DbRecord.Name);
            Assert.AreEqual(v.ValidFrom, o.DbRecord.ValidFrom);
            Assert.AreEqual(v.ValidTo, o.DbRecord.ValidTo);
            Assert.AreEqual(v.Alpha3Code, o.DbRecord.ID);
            Assert.AreEqual(v.CurrencySymbol, o.DbRecord.Code);
        }

        [TestMethod]
        public void CreateNullTest()
        {
            var v = CurrencyViewModelFactory.Create(null);
            Assert.AreEqual(v.Name, Constants.Unspecified);
            Assert.AreEqual(v.ValidFrom, null);
            Assert.AreEqual(v.ValidTo, null);
            Assert.AreEqual(v.CurrencySymbol, Constants.Unspecified);
            Assert.AreEqual(v.Alpha3Code, Constants.Unspecified);
        }

        [TestMethod]
        public void CreateWithExtremumDatesTest()
        {
            var o = GetRandom.Object<CurrencyObject>();
            o.DbRecord.ValidFrom = DateTime.MinValue;
            o.DbRecord.ValidTo = DateTime.MaxValue;
            var v = CurrencyViewModelFactory.Create(o);

            Assert.AreEqual(v.Name, o.DbRecord.Name);
            Assert.AreEqual(v.ValidFrom, null);
            Assert.AreEqual(v.ValidTo, null);
            Assert.AreEqual(v.CurrencySymbol, o.DbRecord.Code);
            Assert.AreEqual(v.Alpha3Code, o.DbRecord.ID);
        }
    }
}
