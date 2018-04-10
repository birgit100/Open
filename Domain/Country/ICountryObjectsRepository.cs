using Open.Core;

namespace Open.Domain.Country
{
    public interface ICountryObjectsRepository : IObjectsRepository<CountryObject>
    {
        bool IsInitialized();
    }
}
