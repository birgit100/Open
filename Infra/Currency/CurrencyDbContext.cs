
using Microsoft.EntityFrameworkCore;
using Open.Data.Money;

namespace Open.Infra.Currency
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> o) : base(o)
        { }

        public DbSet<CurrencyDbRecord> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);
            b.Entity<CurrencyDbRecord>().ToTable("Currency");
        }
    }
}
