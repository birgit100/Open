using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
namespace Open.Tests
{
    public abstract class ObjectsTests<T> : ClassTests<T>
    {
        protected T obj;
        private List<Object> list;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = getRandomTestObject();
            list = GetClass.ReadWritePropertyValues(obj);
        }

        private void validatePropertyValues()
        {
            base.TestCleanup();
            var l = GetClass.ReadWritePropertyValues(obj);
            Assert.AreEqual(l.Count, list.Count);
            for (var i = list.Count; i > 0; i--)
            {
                var e = l[i - 1];
                Assert.IsTrue(list.Contains(e));
                list.Remove(e);
            }
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CanCreateTest()
        {
            Assert.IsNotNull(obj);
        }

        protected void testReadWriteProperty<TR>(Func<TR> get, Func<TR, TR> set, Func<TR> getRandom)
        {
            var x = get();
            Assert.AreEqual(x, get());
            var y = getRandom();
            set(y);
            Assert.AreEqual(y, get());
            Assert.AreNotEqual(x, y);
            list.Remove(x);
            list.Add(y);
            validatePropertyValues();
        }

        protected void testReadWriteProperty<TR>(Func<TR> get, Func<TR, TR> set)
        {
            testReadWriteProperty(get, set, () => (TR)GetRandom.Value(typeof(TR)));
        }

        protected abstract T getRandomTestObject();

    }
}
