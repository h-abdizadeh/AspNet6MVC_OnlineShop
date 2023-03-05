using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShop.Models;

namespace OnlineShop.Classes;

public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    //private readonly DatabaseContext _db;
    private string _roleName;
    public RoleAttribute(/*DatabaseContext db,*/ string roleName)
    {
        //_db = db;
        _roleName = roleName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var userMobile = context.HttpContext.User.Identity.Name;

            DatabaseContext _db = new DatabaseContext();
            var user = _db.Users.FirstOrDefault(u => u.Mobile == userMobile && u.Role.RoleName == _roleName);

            if (user == null)
            {
                context.Result = new RedirectResult("~/Account/register");
            }
        }
        else
        {
            context.Result = new RedirectResult("~/Account/register");
        }
    }
}
