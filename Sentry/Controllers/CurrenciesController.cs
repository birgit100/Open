using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Open.Aids;
using Open.Core;
using Open.Domain.Currency;
using Open.Facade.Currency;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Open.Sentry.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly ICurrencyObjectsRepository repository;
        public const string properties = "Alpha3Code, IsoCurrencyCode, Name, ValidFrom, ValidTo";

        public CurrenciesController(ICurrencyObjectsRepository r) { repository = r; }
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ISOCurrencyParm"] = String.IsNullOrEmpty(sortOrder) ? "isoCurrencyChar_desc" : "";
            ViewData["ISOThreeCharSortParm"] = String.IsNullOrEmpty(sortOrder) ? "isoThreeChar_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
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
                case "isoCurrencyChar_desc":
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
            //int pageSize = 3;
            //return View(await PaginatedList<CurrencyViewModel>.CreateAsync(new CurrencyViewModelsList(l), page ?? 1, pageSize));
            return View( new CurrencyViewModelsList(l));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(properties)] CurrencyViewModel c)
        {
            await validateId(c.Alpha3Code, ModelState);
            if (!ModelState.IsValid) return View(c);
            var o = CurrencyObjectFactory.Create(c.Alpha3Code, c.Name, c.CurrencySymbol, c.ValidFrom, c.ValidTo);
            await repository.AddObject(o);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string id)
        {
            var c = await repository.GetObject(id);
            return View(CurrencyViewModelFactory.Create(c));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(properties)] CurrencyViewModel c)
        {
            if (!ModelState.IsValid) return View(c);
            var o = await repository.GetObject(c.Alpha3Code);
            o.DbRecord.Name = c.Name;
            o.DbRecord.Code = c.CurrencySymbol;
            o.DbRecord.ValidFrom = c.ValidFrom ?? DateTime.MinValue;
            o.DbRecord.ValidTo = c.ValidTo ?? DateTime.MaxValue;
            repository.UpdateObject(o);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var c = await repository.GetObject(id);
            return View(CurrencyViewModelFactory.Create(c));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var c = await repository.GetObject(id);
            return View(CurrencyViewModelFactory.Create(c));
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
            var name = GetMember.DisplayName<CurrencyViewModel>(c => c.Alpha3Code);
            return string.Format(Messages.ValueIsAlreadyInUse, id, name);
        }
    }
}
