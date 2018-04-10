using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Core;
using Open.Domain.Country;
using Open.Facade.Country;

namespace Open.Tests.Facade.Country
{
    [TestClass]
    public class CountryViewModelFactoryTests : BaseTests
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(CountryViewModelFactory);
        }

        [TestMethod]
        public void CreateTest()
        {
            var o = GetRandom.Object<CountryObject>();
            var v = CountryViewModelFactory.Create(o);
            Assert.AreEqual(v.Name, o.DbRecord.Name);
            Assert.AreEqual(v.ValidFrom, o.DbRecord.ValidFrom);
            Assert.AreEqual(v.ValidTo, o.DbRecord.ValidTo);
            Assert.AreEqual(v.Alpha3Code, o.DbRecord.ID);
            Assert.AreEqual(v.Alpha2Code, o.DbRecord.Code);
        }

        [TestMethod]
        public void CreateNullTest()
        {
            var v = CountryViewModelFactory.Create(null);
            Assert.AreEqual(v.Name, Constants.Unspecified);
            Assert.AreEqual(v.ValidFrom, null);
            Assert.AreEqual(v.ValidTo, null);
            Assert.AreEqual(v.Alpha2Code, Constants.Unspecified);
            Assert.AreEqual(v.Alpha3Code, Constants.Unspecified);
        }

        [TestMethod]
        public void CreateWithExtremumDatesTest()
        {
            var o = GetRandom.Object<CountryObject>();
            o.DbRecord.ValidFrom = DateTime.MinValue;
            o.DbRecord.ValidTo = DateTime.MaxValue;
            var v = CountryViewModelFactory.Create(o);

            Assert.AreEqual(v.Name, o.DbRecord.Name);
            Assert.AreEqual(v.ValidFrom, null);
            Assert.AreEqual(v.ValidTo, null);
            Assert.AreEqual(v.Alpha2Code, o.DbRecord.Code);
            Assert.AreEqual(v.Alpha3Code, o.DbRecord.ID);
        }
    }
}
