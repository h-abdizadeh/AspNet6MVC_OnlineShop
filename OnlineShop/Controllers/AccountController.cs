using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;
public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        return View();
    }
}
