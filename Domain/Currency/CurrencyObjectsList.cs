using System.Collections.Generic;
using Open.Data.Money;


namespace Open.Domain.Currency
{
    public class CurrencyObjectsList : List<CurrencyObject>
    {
        public CurrencyObjectsList(IEnumerable<CurrencyDbRecord> l)
        {
            if (l is null) return;
            foreach (var e in l)
            {
                Add(new CurrencyObject(e));
            }
        }
    }
}
