using Open.Core;

namespace Open.Domain.Currency
{
    public interface ICurrencyObjectsRepository : IObjectsRepository<CurrencyObject>
    {
        bool IsInitialized();
    }
}
