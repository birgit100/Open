using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Open.Domain.Currency;

namespace Open.Infra.Currency
{
    public class CurrencyObjectsRepository : ICurrencyObjectsRepository
    {
        private readonly CurrencyDbContext db;

        public CurrencyObjectsRepository(CurrencyDbContext context)
        {
            db = context;
        }

        public async Task<CurrencyObject> GetObject(string id)
        {
            var o = await db.Currencies.FindAsync(id);
            return new CurrencyObject(o);
        }

        public async Task<IEnumerable<CurrencyObject>> GetObjectsList()
        {
            var l = await db.Currencies.ToListAsync();
            return new CurrencyObjectsList(l);
        }

        public async Task<CurrencyObject> AddObject(CurrencyObject o)
        {
            db.Currencies.Add(o.DbRecord);
            await db.SaveChangesAsync();
            return o;
        }

        public async void UpdateObject(CurrencyObject o)
        {
            db.Currencies.Update(o.DbRecord);
            await db.SaveChangesAsync();
        }

        public async void DeleteObject(CurrencyObject o)
        {
            db.Currencies.Remove(o.DbRecord);
            await db.SaveChangesAsync();
        }

        public bool IsInitialized()
        {
            db.Database.EnsureCreated();
            return db.Currencies.Any();
        }
    }
}
