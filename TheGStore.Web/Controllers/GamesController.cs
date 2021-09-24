using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheGStore;
using TheGStore.BLL.Models;
using TheGStore.DAL;
using TheGStore.DAL.Models;

namespace TheGStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly TheGStoreDbContext _context;

        public GamesController(TheGStoreDbContext context)
        {
            _context = context;
        }

        // GET: Software
        public async Task<IActionResult> Index(int id, bool purchased)
        {
            List<Game> games;
            ViewBag.DevId = id;

            if (purchased)
            {
                ViewBag.Purchased = 1;
            }

            if (id == 0)
            {
                games = await _context.Games
                    .Include(s => s.Developer)
                    .ToListAsync();
            }
            else
            {
                ViewBag.Developer = _context.Developers.Find(id).Name;

                games = await _context.Games.Where(s => s.DeveloperId == id)
                    .Include(s => s.Developer)
                    .ToListAsync();
            }

            return View(games);
        }

        // GET: Software/Create
        public IActionResult Create(int devId)
        {
            ViewBag.DevId = devId;
            if (devId != 0)
            {
                ViewBag.Developer = _context.Developers.Find(devId).Name;
            }

            ViewBag.DeveloperList = devId == 0 ?
            new SelectList(_context.Developers, "Id", "Name") :
            new SelectList(new List<Developer>() { _context.Developers.Find(devId) }, "Id", "Name");
            
            return View();
        }

        // POST: Software/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {
            ViewBag.DevId = game.DeveloperId;
            ViewBag.Developer = _context.Developers.Find(game.DeveloperId).Name;

            bool duplicate = _context.Games.Any(s => s.DeveloperId == game.DeveloperId && s.Name.Equals(game.Name));

            if (duplicate)
            {
                ModelState.AddModelError("Name", Resourses.ERROR_GameExists);
            }

            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = game.DeveloperId });
            }

            ViewBag.DeveloperList = new SelectList(_context.Developers, "Id", "Name", game.DeveloperId);

            return View(game);
        }

        // GET: Software/Edit/5
        public async Task<IActionResult> Edit(int id, int devId)
        {
            var software = await _context.Games.FindAsync(id);
            ViewBag.DevId = devId;

            ViewBag.DeveloperList = new SelectList(_context.Developers, "Id", "Name", software.DeveloperId);

            return View(software);
        }

        // POST: Software/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Game game, int devId)
        {
            ViewBag.DevId = devId;
            bool duplicate = _context.Games.Any(s => s.Id != game.Id && s.DeveloperId == game.DeveloperId && s.Name.Equals(game.Name));

            if (duplicate)
            {
                ModelState.AddModelError("Name", Resourses.ERROR_GameExists);
            }

            if (ModelState.IsValid)
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = devId });
            }

            ViewBag.DeveloperList = new SelectList(_context.Developers, "Id", "Name", game.DeveloperId);

            return View(game);
        }

        // POST: Software/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var software = await _context.Games.FindAsync(id);
            _context.Games.Remove(software);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}