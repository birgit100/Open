using System.Collections.Generic;
using Open.Domain.Currency;

namespace Open.Facade.Currency
{
    public class CurrencyViewModelsList : List<CurrencyViewModel>
    {
        public CurrencyViewModelsList(IEnumerable<CurrencyObject> l)
        {
            if (l is null) return;
            foreach (var e in l)
            {
                Add(CurrencyViewModelFactory.Create(e));
            }
        }
    }
}
