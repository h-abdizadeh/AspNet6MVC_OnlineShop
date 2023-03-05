using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Classes;

namespace OnlineShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[RoleAttribute("admin")]
public class PanelController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
