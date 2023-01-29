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
            var products=
                await _context.Products.Where(p=>!p.NotShow &&
                                                  p.Inventory>0 &&
                                                  p.SellOff>0)
                                        .OrderByDescending(p=>p.SellOff)
                                        .Take(4)
                                        .ToListAsync();

            ViewBag.TopProducts=products;

            var groups = 
                await _context.Groups.Where(g => !g.NotShow).Take(5).ToListAsync();

            ViewBag.TopGroups = groups;

            return View();
        }

        public async Task<IActionResult> Product(int id)//product id
        {
            var product = await _context.Products.Include(p=>p.Group).SingleOrDefaultAsync(p=>p.Id==id);

            if (product==null)
            {
                return RedirectToAction(nameof(Index));
            }

            //relative products
            ViewBag.RelativeProducts =
                await _context.Products
                        .Where(p => p.GroupId == product.GroupId && p.Id != product.Id)
                        .Take(4)
                        .ToListAsync();

            return View(product);
        }


        public async Task<IActionResult> Products(int? id)//group id
        {
            ViewBag.ProductTitle = false;

            if (id!=null)//products base group id
            {
                var groupProducts = await _context.Products
                                            .Include(p => p.Group)
                                            .Where(p => !p.NotShow && p.GroupId==id)
                                            .ToListAsync();

                ViewBag.ProductTitle = true;
                return View(groupProducts);
            }

            //all products
            var products = await _context.Products.Where(p=>!p.NotShow).ToListAsync();

            return View(products);
        }


        public IActionResult Ads()
        {
            return PartialView();
        }


    }
}
