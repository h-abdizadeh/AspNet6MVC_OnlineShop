using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineShop.Migrations;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using OnlineShop.Classes;

namespace OnlineShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly DatabaseContext _context;
        public ShopController(DatabaseContext context)
        {
            _context = context;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Guid> AddShopping(int id)//id --> productId
        {


            //var user = await _context.Users.FindAsync(shopping.UserId);

            //change this after login
            var user = await _context.Users.FirstAsync();

            if (user == null) return Guid.Empty; //RedirectToAction("Index", "Home");<-- return action result

            ShoppingViewModel shopping = new ShoppingViewModel()
            {
                ProductId = id,
                UserId = user.Id,
                Count = 1
            };


            //user!=null
            var product = await _context.Products.FindAsync(shopping.ProductId);

            if (product == null) return Guid.Empty; //RedirectToAction("Index", "Home");<-- return action result

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

                return factor.Id;
            }
            else //factor == null
            {
                Factor newFactor = new Factor()
                {
                    Id = Guid.NewGuid(),
                    UserId = shopping.UserId,
                    FactorNumber = new Random().Next(10000, 99999),
                    IsPay = false,
                    State = "در انتظار پرداخت",
                    OpenDate = new Generation().PersianDateTime()
                };

                await _context.Factors.AddAsync(newFactor);

                FactorDetail newDetail = new FactorDetail()
                {
                    Id = Guid.NewGuid(),
                    FactorId = newFactor.Id,
                    ProductId = product.Id,
                    DetailCount = count,
                    FinalPrice = (int)finaPrice * count,
                };

                await _context.FactorDetails.AddAsync(newDetail);
                //await _context.SaveChangesAsync();


                return newFactor.Id;
            }



            return Guid.Empty;
        }
    }
}
