using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
                _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var groups = 
            await _context.Groups.Where(g => !g.NotShow).Take(5).ToListAsync();

            return View();
        }


        public IActionResult Ads()
        {
            return PartialView();
        }


    }
}
