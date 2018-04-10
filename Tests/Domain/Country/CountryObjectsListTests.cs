using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Domain.Country;

namespace Open.Tests.Domain.Country
{
    [TestClass]
    public class CountryObjectsListTests : ObjectsTests<CountryObjectsList>
    {
        protected override CountryObjectsList getRandomTestObject()
        {
            return GetRandom.Object<CountryObjectsList>();
        }

        [TestMethod]
        public void CanCreateWithNullArgumentTest()
        {
            Assert.IsNotNull(new CountryObjectsList(null));
        }

    }
}
