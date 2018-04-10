using Microsoft.EntityFrameworkCore;
using Open.Data.Location;
namespace Open.Infra.Country
{
    public class CountryDbContext : DbContext
    {
        public CountryDbContext(DbContextOptions<CountryDbContext> o) : base(o)
        { }

        public DbSet<CountryDbRecord> Countries { get; set; }
        public object CountryDbRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);
            b.Entity<CountryDbRecord>().ToTable("Country");
        }
    }
}
