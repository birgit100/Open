using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Data.Money;
using Open.Domain.Currency;

namespace Open.Tests.Domain.Currency
{
    [TestClass]
    public class CurrencyObjectTests : ObjectsTests<CurrencyObject>
    {
        protected override CurrencyObject getRandomTestObject()
        {
            return GetRandom.Object<CurrencyObject>();
        }

        [TestMethod]
        public void DbRecordTest()
        {
            var r = GetRandom.Object<CurrencyDbRecord>();
            obj = new CurrencyObject(r);
            Assert.AreSame(r, obj.DbRecord);
        }

        [TestMethod]
        public void CanCreateWithNullTest()
        {
            obj = new CurrencyObject(null);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.DbRecord);
        }

        [TestMethod]
        public void DbRecordIsReadOnlyTest()
        {
            var name = GetMember.Name<CurrencyObject>(x => x.DbRecord);
            Assert.IsTrue(IsReadOnly.Field<CurrencyObject>(name));
        }
    }
}
