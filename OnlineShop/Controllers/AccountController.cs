using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using OnlineShop.Classes;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace OnlineShop.Controllers;
public class AccountController : Controller
{
    private readonly DatabaseContext _context;

    public AccountController(DatabaseContext context)
    {
        _context = context;
    }


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

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel login)
    {

        if (ModelState.IsValid)
        {
            var hashPassword = Security.GetHash(login.Password);

            var user = _context.Users.Include(u=>u.Role)
                               .FirstOrDefault(u => u.Mobile == login.Mobile &&
                                                  u.Password == hashPassword);

            if (user!=null)
            {
                //login to panel Admin or User

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Mobile)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principale=new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true //remember me //مرا به خاطر بسپار
                };

                await HttpContext.SignInAsync(principale, properties);

                if (user.Role.RoleName == "admin")
                    return Redirect("~/Admin/Panel/Index");
                else
                    return Redirect("~/profile/index");
            }
            else
            {
                ModelState.AddModelError("Password", "کاربری با این مشخصات وجود ندارد");
                return View(login);
            }
        }

        return View(login);
    }


    public async Task<IActionResult> SignOutUser()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}
