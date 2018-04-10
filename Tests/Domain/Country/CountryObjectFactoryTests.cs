using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Core;
using Open.Domain.Country;

namespace Open.Tests.Domain.Country
{
    [TestClass]
    public class CountryObjectFactoryTests : BaseTests
    {
        private string id;
        private string name;
        private string code;
        private DateTime validFrom;
        private DateTime validTo;
        private CountryObject o;

        private void initializeTestData()
        {
            var min = DateTime.Now.AddYears(-50);
            var max = DateTime.Now.AddYears(50);
            id = GetRandom.String();
            name = GetRandom.String();
            code = GetRandom.String();
            validFrom = GetRandom.DateTime(min, max);
            validTo = GetRandom.DateTime(validFrom, max);
        }

        private void validateResults(string i = Constants.Unspecified, string n = Constants.Unspecified, string c = Constants.Unspecified, DateTime? f = null, DateTime? t = null)
        {
            Assert.AreEqual(i, o.DbRecord.ID);
            Assert.AreEqual(n, o.DbRecord.Name);
            Assert.AreEqual(c, o.DbRecord.Code);
            Assert.AreEqual(f ?? DateTime.MinValue, o.DbRecord.ValidFrom);
            Assert.AreEqual(t ?? DateTime.MaxValue, o.DbRecord.ValidTo);
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(CountryObjectFactory);
            initializeTestData();
        }

        [TestMethod]
        public void CreateTest()
        {
            o = CountryObjectFactory.Create(id, name, code, validFrom, validTo);
            validateResults(id, name, code, validFrom, validTo);
        }

        [TestMethod]
        public void CreateWithNullArgumentsTest()
        {
            o = CountryObjectFactory.Create(null, null, null);
            validateResults();
        }

        [TestMethod]
        public void CreateWithNullRegionInfoTest()
        {
            o = CountryObjectFactory.Create(null);
            validateResults();
        }

        [TestMethod]
        public void CreateWithRegionInfoTest()
        {
            var i = new RegionInfo("ee-EE");
            o = CountryObjectFactory.Create(i);
            validateResults(i.ThreeLetterISORegionName, i.DisplayName, i.TwoLetterISORegionName);
        }

        [TestMethod]
        public void CreateWithCodeOnlyTest()
        {
            o = CountryObjectFactory.Create(null, null, code);
            validateResults(code, code, code);
        }

        [TestMethod]
        public void CreateWithNameOnlyTest()
        {
            o = CountryObjectFactory.Create(null, name, null);
            validateResults(name, name);
        }

        [TestMethod]
        public void CreateValidFromGreaterThanValidToTest()
        {
            o = CountryObjectFactory.Create(id, name, code, validTo, validFrom);
            validateResults(id, name, code, validFrom, validTo);
        }
    }
}
