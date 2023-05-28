using AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly DatabaseContext _context;

    public PaymentController(DatabaseContext context)
    {
        _context = context;
    }


    [HttpPost]
    public IActionResult Pay()
    {
        var userId = _context.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name).Id;

        var userFactor = _context.Factors.FirstOrDefault(f => f.UserId == userId && f.IsPay == false);

        if (userFactor == null)
            return RedirectToAction("shoppingCart", "profile");


        int amount=userFactor.TotalPrice;
        string[] metadata = { "", "" };
        string description = "خرید از فروشگاه آنلاین";

        return View();
    }


    public IActionResult VerifyPay()
    {
        return View();
    }
}
