using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineShop.Migrations;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly DatabaseContext _context;
        public ShopController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddShopping(ShoppingViewModel shopping)
        {
            var user = await _context.Users.FindAsync(shopping.UserId);

            if (user == null) return RedirectToAction("Index", "Home");

            //user!=null
            var product = await _context.Products.FindAsync(shopping.ProductId);

            if (product == null) return RedirectToAction("Index", "Home");

            //product!=null
            var price = product.Price;
            var sellOff = product.SellOff;
            var count = shopping.Count;
            var finaPrice = price - price * sellOff / 100;

            var factor = await _context.Factors
                        .FirstOrDefaultAsync(f => f.UserId == shopping.UserId
                                                    && f.IsPay == false);

            if (factor != null)
            {
                var detail = await _context.FactorDetails
                            .FirstOrDefaultAsync(d => d.FactorId == factor.Id);

                if (detail != null)
                {
                    count = detail.DetailCount + count;

                    detail.DetailCount = count;
                    detail.FinalPrice = (int)finaPrice * count;
                    detail.Des = shopping.Des;

                    //await _context.SaveChangesAsync();
                    //return Redirect();
                }

                //detail == null
                FactorDetail newDetail = new FactorDetail()
                {
                    Id = Guid.NewGuid(),
                    FactorId = factor.Id,
                    ProductId = product.Id,
                    DetailCount = count,
                    FinalPrice = (int)finaPrice * count,
                    Des = shopping.Des
                };

                await _context.FactorDetails.AddAsync(newDetail);
                //await _context.SaveChangesAsync();
            }
            return View();
        }
    }
}
