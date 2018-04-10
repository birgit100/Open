using Open.Core;

namespace Open.Data.Common
{
    public abstract class IdentifiedDbRecord : UniqueDbRecord
    {
        private string code;
        private string name;

        public string Name
        {
            get => getValue(ref name, Code);
            set => name = value;
        }

        public string Code
        {
            get => getValue(ref code, Constants.Unspecified);
            set => code = value;
        }

        public override string ID
        {
            get => getValue(ref id, Name);
            set => id = value;
        }
    }
}
