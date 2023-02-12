using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

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

    public async Task<IActionResult> ShoppingCart(Guid id)//id --> factor id
    {
        var factor = await _context.Factors.Include(f => f.FactorDetails).FirstOrDefaultAsync(f => f.Id == id && !f.IsPay);

        if (factor!=null)
        {
            return View(factor);
        }

        return RedirectToAction(nameof(Index));
    }
}
