using Open.Aids;
using Open.Domain.Currency;

namespace Open.Infra.Currency
{
    public static class CurrencyDbTableInitializer
    {
        public static void Initialize(ICurrencyObjectsRepository c)
        {
            if (c.IsInitialized()) return;
            var regions = SystemRegionInfo.GetRegionsList();
            foreach (var r in regions)
            {
                if (!SystemRegionInfo.IsCountry(r)) continue;
                var e = CurrencyObjectFactory.Create(r);
                c.AddObject(e);
            }
        }
    }
}
