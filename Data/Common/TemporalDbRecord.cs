using Open.Core;
using System;

namespace Open.Data.Common
{
    public abstract class TemporalDbRecord : RootObject
    {
        private DateTime validFrom = DateTime.MinValue;
        private DateTime validTo = DateTime.MaxValue;

        public DateTime ValidFrom
        {
            get => getMinValue(ref validFrom, ref validTo);
            set => validFrom = value;
        }

        public DateTime ValidTo
        {
            get => getMaxValue(ref validTo, ref validFrom);
            set => validTo = value;
        }
    }
}
