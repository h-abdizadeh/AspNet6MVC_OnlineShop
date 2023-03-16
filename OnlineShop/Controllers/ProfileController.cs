using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly DatabaseContext _context;

    public ProfileController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ShoppingCart(Guid? id)//id --> factor id
    {
        var factor =await _context.Factors
                        .Where(f => f.Id == id && !f.IsPay)
                        .Select(s => new Factor()
                        {
                            Id = s.Id,
                            OpenDate = s.OpenDate,
                            IsPay = s.IsPay,
                            State = s.State,
                            UserId = s.UserId,
                            FactorNumber = s.FactorNumber,
                            FactorDetails = s.FactorDetails.Select(d => new FactorDetail()
                            {
                                Id = d.Id,
                                FactorId = d.FactorId,
                                DetailCount = d.DetailCount,
                                ProductId = d.ProductId,
                                Des = s.Des,
                                FinalPrice = d.FinalPrice,
                                Product = d.Product
                            }).ToList()
                        }).FirstOrDefaultAsync();

        if (factor!=null)
        {
            return View(factor);
        }


        ////// check factor by user Id
        var userId = _context.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name).Id;

        var userFactor = await _context.Factors
                         .Where(f => f.UserId == userId && !f.IsPay)
                         .Select(s => new Factor()
                         {
                             Id = s.Id,
                             OpenDate = s.OpenDate,
                             IsPay = s.IsPay,
                             State = s.State,
                             UserId = s.UserId,
                             FactorNumber = s.FactorNumber,
                             FactorDetails = s.FactorDetails.Select(d => new FactorDetail()
                             {
                                 Id = d.Id,
                                 FactorId = d.FactorId,
                                 DetailCount = d.DetailCount,
                                 ProductId = d.ProductId,
                                 Des = s.Des,
                                 FinalPrice = d.FinalPrice,
                                 Product = d.Product
                             }).ToList()
                         }).FirstOrDefaultAsync();

        if (userFactor!=null)
        {
            return View(userFactor);
        }


        return RedirectToAction(nameof(Index));
    }

}
