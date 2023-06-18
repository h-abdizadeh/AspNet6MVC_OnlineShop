using Dto.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using ZarinPal.Class;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Numerics;

namespace OnlineShop.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private static readonly HttpClient client = new HttpClient();

    private readonly Payment _payment;
    private readonly Authority _authority;
    private readonly Transactions _transactions;

    private readonly DatabaseContext _context;
    public PaymentController(DatabaseContext context)
    {
        _context = context;
        var expose = new Expose();
        _payment = expose.CreatePayment();
        _authority = expose.CreateAuthority();
        _transactions = expose.CreateTransactions();
    }


    public async Task<IActionResult> Pay()
    {
        //var payment = new Payment();
        var userMobile = User.Identity.Name;

        var user = _context.Users.FirstOrDefault(u => u.Mobile == userMobile);

        if (user == null) return RedirectToAction("index", "home");

        var factor =
        _context.Factors.Include(f => f.FactorDetails).FirstOrDefault(f => f.UserId == user.Id && !f.IsPay);


        if (factor == null) return RedirectToAction("index", "home");


        var result = await _payment.Request(new DtoRequest()
        {
            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
            Amount = factor.FactorDetails.Sum(f => f.FinalPrice),
            Description = "خرید تستی ",
            CallbackUrl =
            "https://localhost:7003/payment/VerifyPay?factorId=" + factor.Id,
            Email = "",
            Mobile = ""
        }, Payment.Mode.sandbox);//final project use Mode.zarinpal

        if (result.Status == 100)
        {
            //return Redirect("https://www.zarinpal.com/pg/StartPay/" + result.Authority);
            return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Authority);
        }

        return View("Error");
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyPay(Guid factorId, string status, string authority)
    {
        if (status.ToLower() == "ok")
        {
            ViewBag.Result = "ok";

            var factor =
                _context.Factors.Include(f => f.FactorDetails)
                                .FirstOrDefault(f => f.Id == factorId);


            if (factor == null)
                return RedirectToAction("shoppingcart", "profile");

            var verify = await _payment.Verification(new DtoVerification()
            {
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Amount = factor.FactorDetails.Sum(f => f.FinalPrice),
                Authority = authority
            }, Payment.Mode.sandbox);


            //if (verify.Status==100)
            //{
            //    factor.IsPay = true;
            //}
            //else
            //{
            //    factor.IsPay = false;
            //}

            factor.IsPay = verify.Status == 100;

            if (factor.IsPay)
            {
                foreach (var item in factor.FactorDetails)
                {
                    var product = _context.Products.Find(item.ProductId);
                    product.Inventory -= (uint)item.DetailCount;
                }

                factor.Des = verify.RefId.ToString();
                await _context.SaveChangesAsync();
            }

        }
        else
        {
            ViewBag.Result = "error";
        }

        return View();
    }

    //public IActionResult VerifyPay()
    //{
    //    return View();
    //}
}
