using Open.Data.Location;

namespace Open.Domain.Country
{
    public class CountryObject
    {
        public readonly CountryDbRecord DbRecord;

        public CountryObject(CountryDbRecord r)
        {
            DbRecord = r?? new CountryDbRecord();
        }
    }
}
