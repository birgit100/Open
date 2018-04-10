using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Open.Domain.Country;
namespace Open.Infra.Country
{
    public class CountryObjectsRepository : ICountryObjectsRepository
    {
        private readonly CountryDbContext db;

        public CountryObjectsRepository(CountryDbContext context)
        {
            db = context;
        }

        public async Task<CountryObject> GetObject(string id)
        {
            var o = await db.Countries.FindAsync(id);
            return new CountryObject(o);
        }

        public async Task<IEnumerable<CountryObject>> GetObjectsList()
        {
            var l = await db.Countries.ToListAsync();
            return new CountryObjectsList(l);
        }

        public async Task<CountryObject> AddObject(CountryObject o)
        {
            db.Countries.Add(o.DbRecord);
            await db.SaveChangesAsync();
            return o;
        }

        public async void UpdateObject(CountryObject o)
        {
            db.Countries.Update(o.DbRecord);
            await db.SaveChangesAsync();
        }

        public async void DeleteObject(CountryObject o)
        {
            db.Countries.Remove(o.DbRecord);
            await db.SaveChangesAsync();
        }

        public bool IsInitialized()
        {
            db.Database.EnsureCreated();
            return db.Countries.Any();
        }
    }
}
