using Dto.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineShop.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZarinPal.Class;
using Microsoft.EntityFrameworkCore;

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

    //int amount;
    //string merchantId = "YOUR-ZARINPAL-MERCHANT-CODE";//"cfa83c81-99b0-4893-9465-2c3fcd323455";//"YOUR-ZARINPAL-MERCHANT-CODE";
    //string[] metadata = { "", "" };
    string merchantId = "YOUR-ZARINPAL-MERCHANT-CODE";
    string amount = "1100";
    string description = "خرید تستی ";
    string callBackUrl = "https://localhost:44301/payment/VerifyPay";
    string authority;

  
    public async Task<IActionResult> Pay()
    {
        //var payment = new Payment();
        var userMobile = User.Identity.Name;

        var user = _context.Users.FirstOrDefault(u => u.Mobile == userMobile);

        if (user == null) return RedirectToAction("index", "home");

        var factor =
        _context.Factors.Include(f=>f.FactorDetails).FirstOrDefault(f => f.UserId == user.Id && !f.IsPay);


        if (factor == null) return RedirectToAction("index", "home");


        var result = await _payment.Request(new DtoRequest()
        {
            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
            Amount = factor.FactorDetails.Sum(f=>f.FinalPrice),
            Description = "خرید تستی ",
            CallbackUrl = 
            "https://localhost:7003/payment/VerifyPay?factorId="+factor,
            Email = "",
            Mobile = ""
        }, Payment.Mode.sandbox);

        if (result.Status == 100)
        {
            return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Authority);
        }

        return View("Error");
    }

    public IActionResult VerifyPay(Guid factorId)
    {
        return View();
    }
}

//public class ZarinPalRequestResponseModel
//{
//    public int Status { get; set; }
//    public string Authority { get; set; }
//}

//public class URLs
//{
//    public const String gateWayUrl = "https://sandbox.zarinpal.com/pg/StartPay/";
//    public const String requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
//    public const String verifyUrl = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";


//}

//public class RequestParameters
//{
//    public string merchant_id { get; set; }

//    public string amount { get; set; }
//    public string description { get; set; }
//    public string callback_url { get; set; }

//    public string[]? metadata { get; set; }

//    public RequestParameters(string merchant_id, string amount, string description, string callback_url, string? mobile, string? email)
//    {
//        this.merchant_id = merchant_id;
//        this.amount = amount;
//        this.description = description;
//        this.callback_url = callback_url;
//        this.metadata = new string[2];
//        if (mobile != null)
//        {
//            this.metadata[0] = mobile;
//        }
//        if (email != null)
//        {
//            this.metadata[1] = email;
//        }


//    }
//}