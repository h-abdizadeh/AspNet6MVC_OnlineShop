﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
                _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products=
                await _context.Products.Where(p=>!p.NotShow &&
                                                  p.Inventory>0 &&
                                                  p.SellOff>0)
                                        .OrderByDescending(p=>p.SellOff)
                                        .Take(4)
                                        .ToListAsync();

            ViewBag.TopProducts=products;

            var groups = 
                await _context.Groups.Where(g => !g.NotShow).Take(5).ToListAsync();

            ViewBag.TopGroups = groups;

            return View();
        }


        public IActionResult Ads()
        {
            return PartialView();
        }


    }
}
