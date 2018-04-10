
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Facade.Country;

namespace Open.Tests.Facade.Country
{
    [TestClass]
    public class CountryViewModelTests : ObjectsTests<CountryViewModel>
    {
        [TestMethod]
        public new void CanCreateTest()
        {
            Assert.IsNotNull(new CountryViewModel());
        }

        protected override CountryViewModel getRandomTestObject()
        {
            return GetRandom.Object<CountryViewModel>();
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
        public void Alpha2CodeTest()
        {
            testReadWriteProperty(() => obj.Alpha2Code, x => obj.Alpha2Code = x);
        }
    }
}
