using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGStore;
using TheGStore.BLL.Models;
using TheGStore.DAL;
using TheGStore.DAL.Models;

namespace TheGStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TheGStoreDbContext _context;

        public OrdersController(TheGStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            List<Order> orders;
            List<Game> game;

            ViewBag.CustomerId = id;

            if (id == 0)
            {
                orders = await _context.Orders.Include(p => p.Customer).Include(p => p.Game).ToListAsync();
            }
            else
            {
                ViewBag.Customer = _context.Customers.Find(id).Email;
                orders = await _context.Orders.Where(p => p.CustomerId == id).Include(p => p.Game).ToListAsync();
            }

            foreach (var p in orders)
            {
                game = await _context.Games.Where(s => s.Id == p.GameId).Include(s => s.Developer).ToListAsync();
                p.Game = game[0];
            }

            return View(orders);
        }

        public IActionResult Purchase(int gameId, int devId)
        {
            FillViewBag(gameId, devId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase(Customer model, int gameId, int devId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email.Equals(model.Email));
            bool duplicate = customer == null ? false : _context.Orders.Any(p => p.GameId == gameId && p.CustomerId == customer.Id);

            if (duplicate)
            {
                ModelState.AddModelError("Email", Resourses.ERROR_ALREADY_BOUGHT);
            }

            if (ModelState.IsValid)
            {
                if (customer == null)
                {
                    customer = model;
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                }

                var purchase = new Order() { CustomerId = customer.Id, GameId = gameId, Date = DateTime.Now };
                await _context.Orders.AddAsync(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Games", new { id = devId, purchased = true });
            }

            FillViewBag(gameId, devId);
            return View(model);
        }

        public void FillViewBag(int gameId, int devId)
        {
            ViewBag.DevId = devId;
            ViewBag.GameId = gameId;
            ViewBag.Software = _context.Games.Find(gameId).Name;
        }
    }
}