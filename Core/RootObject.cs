using Open.Aids;
using System;

namespace Open.Core
{
    public abstract class RootObject
    {

        protected internal string getValue(ref string field, string value)
        {
            if (string.IsNullOrWhiteSpace(field)) field = (value ?? string.Empty).Trim();
            return field;
        }

        protected internal T getMinValue<T>(ref T field, ref T value) where T : IComparable
        {
            ToTheSequence.OfGrowing(ref field, ref value);
            return field;
        }

        protected internal T getMaxValue<T>(ref T field, ref T value) where T : IComparable
        {
            ToTheSequence.OfGrowing(ref value, ref field);
            return field;
        }
    }
}
