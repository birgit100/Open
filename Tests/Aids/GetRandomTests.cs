using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Aids;
using Open.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using Open.Data.Location;

namespace Open.Tests.Aids
{
    [TestClass]
    public class GetRandomTests : BaseTests
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            type = typeof(GetRandom);
        }

        [TestMethod]
        public void DoubleTest()
        {
            var d = 10000; 
            doGetRandomTest(GetRandom.Double, (double)10, 110);
            doGetRandomTest(GetRandom.Double, (double)-110, -10);
            doGetRandomTest(GetRandom.Double, (double)-50, 50);
            doGetRandomTest(GetRandom.Double, double.MinValue, double.MaxValue);
            doGetRandomTest(GetRandom.Double, double.MaxValue / d, double.MaxValue);
            doGetRandomTest(GetRandom.Double, double.MinValue, double.MinValue / d);
        }

        [TestMethod]
        public void FloatTest()
        {
            var d = 10F;
            doGetRandomTest(GetRandom.Float, 10F, 110F);
            doGetRandomTest(GetRandom.Float, -110F, -10F);
            doGetRandomTest(GetRandom.Float, float.MinValue, float.MaxValue);
            doGetRandomTest(GetRandom.Float, float.MaxValue / d, float.MaxValue);
            doGetRandomTest(GetRandom.Float, float.MinValue, float.MinValue / d);
        }

        [TestMethod]
        public void Int8Test()
        {
            doGetRandomTest(GetRandom.Int8, (sbyte)10, (sbyte)110);
            doGetRandomTest(GetRandom.Int8, (sbyte)-110, (sbyte)-10);
            doGetRandomTest(GetRandom.Int8, (sbyte)-50, (sbyte)50);
            doGetRandomTest(GetRandom.Int8, sbyte.MinValue, (sbyte)(sbyte.MinValue + 100));
            doGetRandomTest(GetRandom.Int8, (sbyte)(sbyte.MaxValue - 100), sbyte.MaxValue);
            doGetRandomTest(GetRandom.Int8, sbyte.MinValue, sbyte.MaxValue);
        }

        [TestMethod]
        public void Int16Test()
        {
            doGetRandomTest(GetRandom.Int16, (short) 100, (short)200);
            doGetRandomTest(GetRandom.Int16, (short)-200, (short)100);
            doGetRandomTest(GetRandom.Int16, (short)-400, (short)-200);
            doGetRandomTest(GetRandom.Int16, short.MinValue, (short) (short.MinValue +200));
            doGetRandomTest(GetRandom.Int16, (short) (short.MaxValue - 100), short.MaxValue);
            doGetRandomTest(GetRandom.Int16, short.MinValue, short.MaxValue);
        }

        [TestMethod]
        public void Int32Test()
        {
            doGetRandomTest(GetRandom.Int32, 100, 200);
            doGetRandomTest(GetRandom.Int32, -200, 100);
            doGetRandomTest(GetRandom.Int32, -400, -200);
            doGetRandomTest(GetRandom.Int32, int.MinValue, int.MinValue + 200);
            doGetRandomTest(GetRandom.Int32, int.MaxValue - 100, int.MaxValue);
            doGetRandomTest(GetRandom.Int32, int.MinValue, int.MaxValue);
        }

        [TestMethod]
        public void Int64Test()
        {
            var d = 1000000L;
            doGetRandomTest(GetRandom.Int64, (long)100, 200);
            doGetRandomTest(GetRandom.Int64, (long) -200, 100);
            doGetRandomTest(GetRandom.Int64, (long) -400, -200);
            doGetRandomTest(GetRandom.Int64, long.MinValue, long.MaxValue);
            doGetRandomTest(GetRandom.Int64, long.MinValue, long.MinValue + d);
            doGetRandomTest(GetRandom.Int64, long.MaxValue - d, long.MaxValue);
        }


        [TestMethod]
        public void UInt8Test()
        {
            doGetRandomTest(GetRandom.UInt8, (byte) 10, (byte)110);
            doGetRandomTest(GetRandom.UInt8, byte.MinValue, (byte) (byte.MinValue +100));
            doGetRandomTest(GetRandom.UInt8, (byte) (byte.MaxValue - 100), byte.MaxValue);
            doGetRandomTest(GetRandom.UInt8, byte.MinValue, byte.MaxValue);
        }

        [TestMethod]
        public void UInt16Test()
        {
            doGetRandomTest(GetRandom.UInt16, (ushort) 100, (ushort) 200);
            doGetRandomTest(GetRandom.UInt16, ushort.MinValue, (ushort) (ushort.MinValue + 200));
            doGetRandomTest(GetRandom.UInt16, (ushort) (ushort.MaxValue - 100), ushort.MaxValue);
            doGetRandomTest(GetRandom.UInt16, ushort.MinValue, ushort.MaxValue);
        }

        [TestMethod]
        public void UInt32Test()
        {
            doGetRandomTest(GetRandom.UInt32, (uint) 100, (uint) 200);
            doGetRandomTest(GetRandom.UInt32, uint.MinValue, uint.MinValue+200);
            doGetRandomTest(GetRandom.UInt32, uint.MaxValue -100, uint.MaxValue);
            doGetRandomTest(GetRandom.UInt32, uint.MinValue, uint.MaxValue);
        }

        [TestMethod]
        public void UInt64Test()
        {
            var d = 1000000UL;
            doGetRandomTest(GetRandom.UInt64, (ulong)100, (ulong)200);
            doGetRandomTest(GetRandom.UInt64, ulong.MinValue, ulong.MaxValue);
            doGetRandomTest(GetRandom.UInt64, ulong.MaxValue - d, ulong.MaxValue);
            doGetRandomTest(GetRandom.UInt64, ulong.MinValue, ulong.MaxValue + d);
        }

        [TestMethod]
        public void DecimalTest()
        {
            var d = 10M;
            doGetRandomTest(GetRandom.Decimal, 100M, 200M);
            doGetRandomTest(GetRandom.Decimal, -200M, 100M);
            doGetRandomTest(GetRandom.Decimal, -400M, -200M);
            doGetRandomTest(GetRandom.Decimal, decimal.MinValue, decimal.MaxValue);
            doGetRandomTest(GetRandom.Decimal, decimal.MinValue, decimal.MinValue/d);
            doGetRandomTest(GetRandom.Decimal, decimal.MaxValue/d, decimal.MaxValue);
        }

        [TestMethod]
        public void BoolTest()
        {
            var b = GetRandom.Bool();
            Assert.IsInstanceOfType(b, typeof(bool));
            while (true)
            {
                if (GetRandom.Bool() == !b)
                    return;
            }
        }

        [TestMethod]
        public void CharTest()
        {
            doGetRandomTest(GetRandom.Char, 'a', 'z');
            doGetRandomTest(GetRandom.Char, 'A', 'Z');
            doGetRandomTest(GetRandom.Char, char.MinValue, char.MaxValue);
            doGetRandomTest(GetRandom.Char, char.MinValue, (char) (char.MinValue + 100));
            doGetRandomTest(GetRandom.Char, (char) (char.MaxValue - 100), char.MaxValue);
        }

        [TestMethod]
        public void TimeSpanTest()
        {
            doTests(GetRandom.TimeSpan);
        }

        [TestMethod]
        public void DateTimeTest()
        {
            var now = DateTime.Now;
            var min = DateTime.MinValue;
            var max = DateTime.MaxValue;
            doGetRandomTest((x,y) => GetRandom.DateTime(x, y), now.AddYears(-5), now.AddYears(5));
            doGetRandomTest((x, y) => GetRandom.DateTime(x, y), min, min.AddYears(10));
            doGetRandomTest((x, y) => GetRandom.DateTime(x, y), max.AddYears(-10), max);
            doGetRandomTest((x, y) => GetRandom.DateTime(x, y), min, max);
        }

        [TestMethod]
        public void ColorTest()
        {
            doTests(GetRandom.Color);
        }

        [TestMethod]
        public void StringTest()
        {
            doTests(() => GetRandom.String());
        }

        [TestMethod]
        public void ValueTest()
        {
            void test(Type x, Type y = null)
            {
                Assert.IsInstanceOfType(GetRandom.Value(x), y??x);
                if (y is null) return;
                Assert.IsInstanceOfType(GetRandom.Value(y), y);
            }
            test(typeof(bool?), typeof(bool));
            test(typeof(char?), typeof(char));
            test(typeof(Color?), typeof(Color));
            test(typeof(DateTime?), typeof(DateTime));
            test(typeof(decimal?), typeof(decimal));
            test(typeof(double?), typeof(double));
            test(typeof(IsoGender?), typeof(IsoGender));
            test(typeof(float?), typeof(float));
            test(typeof(sbyte?), typeof(sbyte));
            test(typeof(short?), typeof(short));
            test(typeof(int?), typeof(int));
            test(typeof(long?), typeof(long));
            test(typeof(TimeSpan?), typeof(TimeSpan));
            test(typeof(byte?), typeof(byte));
            test(typeof(ushort?), typeof(ushort));
            test(typeof(uint?), typeof(uint));
            test(typeof(ulong?), typeof(ulong));
            test(typeof(string));
            test(typeof(object));
            test(typeof(CountryDbRecord));
        }

        [TestMethod]
        public void EnumTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ObjectTest()
        {
            Assert.Inconclusive();
        }

        private static void doGetRandomTest<T>(Func<T, T, T> funct, T min, T max) where T : IComparable
        {
                doTests(funct, min);
                doTests(funct, max);
                doTests(funct, min, max);
                doTests((x, y) => funct(max, min), min, max);
        }

        private static void doTests<T>(Func<T, T, T> funct, T min, T max) where T : IComparable
        {
            var l = new List<T>();
            for (var i = 0; i < 10; i++)
            {
                T r;
                do
                { r = funct(min, max); } while (l.Contains(r));
                Assert.IsInstanceOfType(r, typeof(T));
                Assert.IsTrue(r.CompareTo(min) >= 0);
                Assert.IsTrue(r.CompareTo(max) <= 0);
                l.Add(r);
            }
        }

        private static void doTests<T>(Func<T> funct)
        {
            var l = new List<T>();
            for (var i = 0; i < 10; i++)
            {
                T r;
                do
                { r = funct(); } while (l.Contains(r));
                Assert.IsInstanceOfType(r, typeof(T));
                l.Add(r);
             }
        }

        private static void doTests<T>(Func<T, T, T> funct, T x)
        {
            Assert.AreEqual(x, funct(x, x));
        }
    } 
    
          
}



