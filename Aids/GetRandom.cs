using System;
using System.Drawing;
using System.Text;
namespace Open.Aids
{
    public static class GetRandom
    {
        private static readonly Random r = new Random();

        public static double Double( double min = double.MinValue, double max = double.MaxValue)
        {
            if (min.CompareTo(max) == 0) return min;
            ToTheSequence.OfGrowing(ref min, ref max);
            var d = r.NextDouble();
            if (max > 0) return min + d * max - d * min;
            return min - d * min + d * max;
        }

        public static int Int32(int min = int.MinValue, int max = int.MaxValue)
        {
            if (min.CompareTo(max) == 0) return min;
            if (min.CompareTo(max) > 0) return r.Next(max, min);
            return r.Next(min, max);
        }

        public static sbyte Int8(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
        {
            return (sbyte) Int32(min, max);
        }

        public static short Int16(short min = short.MinValue, short max = short.MaxValue)
        {
            return (short) Int32(min, max);
        }
        public static float Float(float min = float.MinValue, float max = float.MaxValue)
        {
            return Convert.ToSingle(Double(min, max));
        }

        public static byte UInt8(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return (byte) Int32(min, max);
        }

        public static ushort UInt16(ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
        {
            return (ushort) Int32(min, max);
        }

        public static uint UInt32(uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            return Convert.ToUInt32(Double(min, max));
        }

        public static ulong UInt64(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
        {
            if (min == max) return min;
            return Safe.Run(() => Convert.ToUInt64(Double(Convert.ToDouble(min), Convert.ToDouble(max))), min);
        }

        public static long Int64(long min = long.MinValue, long max = long.MaxValue)
        {
            if (min == max) return min;
            return Safe.Run(() => Convert.ToInt64(Double(Convert.ToDouble(min), Convert.ToDouble(max))), min);
        }

        public static decimal Decimal(decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            if (min == max) return min;
            return Safe.Run(() => Convert.ToDecimal(Double(Convert.ToDouble(min), Convert.ToDouble(max))), min);
        }

        public static bool Bool()
        {
            return Int32() % 2 == 0;
        }

        public static char Char(char min = char.MinValue, char max = System.Char.MaxValue)
        {
            return (char) UInt16(min, max);
        }

        public static TimeSpan TimeSpan()
        {
            return new TimeSpan(Int64());
        }

        public static DateTime DateTime(DateTime? minValue = null, DateTime? maxValue = null)
        {
            var min = minValue ?? System.DateTime.MinValue;
            var max = maxValue ?? System.DateTime.MaxValue;
            var d = new DateTime(Int64(min.Ticks, max.Ticks));
            if (d.Hour == 3) d = d.AddHours(UInt8(4, 22));
            return d;
        }

        public static Color Color()
        {
            return System.Drawing.Color.FromArgb(UInt8(), UInt8(), UInt8());
        }

        public static string String(byte minLength = 3, byte maxLength = 5)
        {
            var b = new StringBuilder();
            var size = UInt8(minLength, maxLength);
            for (var i = 0; i < size; i++) b.Append(Char('a', 'z'));
            return b.ToString();
        }

        public static object Value(Type t)
        {
            var x = Nullable.GetUnderlyingType(t);
            if (!(x is null)) t = x;
            if (t.IsEnum) return Enum(t);
            if (t == typeof(string)) return String();
            if (t == typeof(char)) return Char();
            if (t == typeof(Color)) return Color();
            if (t == typeof(bool)) return Bool();
            if (t == typeof(DateTime)) return DateTime();
            if (t == typeof(decimal)) return Decimal();
            if (t == typeof(double)) return Double();
            if (t == typeof(float)) return Float();
            if (t == typeof(byte)) return UInt8();
            if (t == typeof(ushort)) return UInt16();
            if (t == typeof(uint)) return UInt32();
            if (t == typeof(ulong)) return UInt64();
            if (t == typeof(sbyte)) return Int8();
            if (t == typeof(short)) return Int16();
            if (t == typeof(int)) return Int32();
            if (t == typeof(long)) return Int64();
            if (t == typeof(TimeSpan)) return TimeSpan();
            return Object(t);
        }

        public static T Enum<T>()
        {
            return (T)Enum(typeof(T));
        }

        private static object Enum(Type t)
        {
            var count = GetEnum.Count(t);
            var index = Int32(0, count);
            return GetEnum.Value(t, index);
        }

        public static object Object(Type t)
        {
            var o = CreateNew.Instance(t);
            SetRandom.Values(o);
            return o;
        }

        public static T Object<T>()
        {
            var o = CreateNew.Instance<T>();
            SetRandom.Values(o);
            return o;
        }

    }
}
