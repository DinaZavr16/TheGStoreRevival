using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheGStore.BLL.Contracts;
using TheGStore.Bll.Models;

namespace TheGStore.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryManager _countryManager;

        public CountriesController(ICountryManager countryManager)
        {
            _countryManager = countryManager;
        }

        // GET: Games
        public async Task<IActionResult> Index(int pageIndex, int pageSize)
        {
            return View(await _countryManager.GetList(pageIndex,pageSize));
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Country = _countryManager.GetById(id);
            return View();
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryModel country)
        {
            await _countryManager.Create(country);

            return View(country);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var countries = await _countryManager.GetById(id);
            return View(countries);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountryUpdateModel country)
        {
            await _countryManager.Update(country);

            return View(country);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _countryManager.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}