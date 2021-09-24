using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGStore;
using TheGStore.DAL;

namespace TheGStore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly TheGStoreDbContext _context;

        public CustomersController(TheGStoreDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }
    }
}