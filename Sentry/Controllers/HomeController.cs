using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Open.Infra.Country;
using Open.Infra.Currency;
using Open.Sentry.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace Open.Sentry.Controllers
{
    public class HomeController : Controller
    {
        private readonly CountryDbContext countryContext;
        private readonly CurrencyDbContext currencyContext;

        public HomeController(CountryDbContext coContext, CurrencyDbContext cuContext)
        {
            countryContext = coContext;
            currencyContext = cuContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            IQueryable<ValidFromGroup> data =
                from country in countryContext.CountryDbRecord
                group country by country.ValidFromDate into dateGroup
                select new ValidFromGroup()
                {
                    ValidFromDate = dateGroup.Key,
                    ObjectCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
