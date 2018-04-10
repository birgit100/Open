using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Open.Infra.Country;
using Open.Sentry;
using Open.Sentry.Data;

namespace Open.Tests.Sentry
{
    public class TestStartup : Startup
    {
        public const string Testing = "Testing";

        public TestStartup(IConfiguration configuration) : base(configuration) { }

        protected override void setDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(Testing));
            services.AddDbContext<CountryDbContext>(options => options.UseInMemoryDatabase(Testing));
        }

        protected override void setAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test Scheme";
                options.DefaultChallengeScheme = "Test Scheme";
            }).AddTestAuth(o => { });
        }
    }
}
