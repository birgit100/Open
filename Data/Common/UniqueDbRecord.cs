using Open.Core;

namespace Open.Data.Common
{
    public abstract class UniqueDbRecord : TemporalDbRecord
    {
        protected string id;

        public virtual string ID
        {
            get => getValue(ref id, Constants.Unspecified);
            set => id = value;
        }
    }
}
