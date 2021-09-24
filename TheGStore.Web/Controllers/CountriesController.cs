using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheGStore.DAL.Models;
using TheGStore;
using TheGStore.DAL;
using TheGStore.BLL.Models;

namespace TheGStore.Controllers
{
    public class CountriesController : Controller
    {
        private readonly TheGStoreDbContext _context;

        public CountriesController(TheGStoreDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Country = _context.Countries.Find(id).Name;
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
        public async Task<IActionResult> Create(Country country)
        {
            bool duplicate = await _context.Countries.AnyAsync(d => d.Name.Equals(country.Name));

            if (duplicate)
            {
                ModelState.AddModelError("FirstName", Resourses.ERROR_CountryExists);
            }

            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var countries = await _context.Countries.FindAsync(id);
            return View(countries);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            bool duplicate = await _context.Countries.AnyAsync(d => d.Name.Equals(country.Name));

            if (duplicate)
            {
                ModelState.AddModelError("FirstName", Resourses.ERROR_CountryExists);
            }

            if (ModelState.IsValid)
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countries = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(countries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}