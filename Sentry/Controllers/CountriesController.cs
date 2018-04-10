using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Open.Aids;
using Open.Core;
using Open.Domain.Country;
using Open.Facade.Country;

namespace Open.Sentry.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryObjectsRepository repository;
        public const string properties = "Alpha3Code, Alpha2Code, Name, ValidFrom, ValidTo";

        public CountriesController(ICountryObjectsRepository r) { repository = r; }
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ISOTwoCharParm"] = String.IsNullOrEmpty(sortOrder) ? "isoTwoChar_desc" : "";
            ViewData["ISOThreeCharSortParm"] = String.IsNullOrEmpty(sortOrder) ? "isoThreeChar_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["CurrentFilter"] = searchString;

            var l = await repository.GetObjectsList();

            if (!String.IsNullOrEmpty(searchString))
            {
                l = l.Where(s => s.DbRecord.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    l = l.OrderByDescending(s => s.DbRecord.Name);
                    break;
                case "isoTwoChar_desc":
                    l = l.OrderByDescending(s => s.DbRecord.Code);
                    break;
                case "isoThreeChar_desc":
                    l = l.OrderByDescending(s => s.DbRecord.ID);
                    break;
                case "date_desc":
                    l = l.OrderByDescending(s => s.DbRecord.ValidFrom);
                    break;
                case "date_asc":
                    l = l.OrderBy(s => s.DbRecord.ValidFrom);
                    break;
                default:
                    l = l.OrderBy(s => s.DbRecord.Name);
                    break;
            }
            return View(new CountryViewModelsList(l));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(properties)] CountryViewModel c)
        {
            await validateId(c.Alpha3Code, ModelState);
            if (!ModelState.IsValid) return View(c);
            var o = CountryObjectFactory.Create(c.Alpha3Code, c.Name, c.Alpha2Code, c.ValidFrom, c.ValidTo);
            await repository.AddObject(o);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string id)
        {
            var c = await repository.GetObject(id);
            return View(CountryViewModelFactory.Create(c));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(properties)] CountryViewModel c)
        {
            if (!ModelState.IsValid) return View(c);
            var o = await repository.GetObject(c.Alpha3Code);
            o.DbRecord.Name = c.Name;
            o.DbRecord.Code = c.Alpha2Code;
            o.DbRecord.ValidFrom = c.ValidFrom ?? DateTime.MinValue;
            o.DbRecord.ValidTo = c.ValidTo ?? DateTime.MaxValue;
            repository.UpdateObject(o);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var c = await repository.GetObject(id);
            return View(CountryViewModelFactory.Create(c));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var c = await repository.GetObject(id);
            return View(CountryViewModelFactory.Create(c));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var c = await repository.GetObject(id);
            repository.DeleteObject(c);
            return RedirectToAction("Index");
        }

        private async Task validateId(string id, ModelStateDictionary d)
        {
            if (await isIdInUse(id))
                d.AddModelError(string.Empty, idIsInUseMessage(id));
        }

        private async Task<bool> isIdInUse(string id)
        {
            return (await repository.GetObject(id))?.DbRecord?.ID == id;
        }

        private static string idIsInUseMessage(string id)
        {
            var name = GetMember.DisplayName<CountryViewModel>(c => c.Alpha3Code);
            return string.Format(Messages.ValueIsAlreadyInUse, id, name);
        }
    }
}