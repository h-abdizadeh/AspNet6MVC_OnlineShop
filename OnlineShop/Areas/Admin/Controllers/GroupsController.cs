using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Classes;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[RoleAttribute("admin")]
public class GroupsController : Controller
{
    private readonly DatabaseContext _context;

    public GroupsController(DatabaseContext context)
    {
        _context = context;
    }

    // GET: Admin/Groups
    public async Task<IActionResult> Index()
    {
        return View(await _context.Groups.ToListAsync());
    }

    // GET: Admin/Groups/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Groups == null)
        {
            return NotFound();
        }

        var @group = await _context.Groups.FirstOrDefaultAsync(m => m.Id == id);
        if (@group == null)
        {
            return NotFound();
        }

        return View(@group);
    }

    // GET: Admin/Groups/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Groups/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Group @group, IFormFile imgFile)
    {
        //if (!string.IsNullOrEmpty(group.Name))
        if (ModelState.IsValid)
        {
            if (imgFile != null)
            {
                Random r = new Random();
                int prefix = r.Next(1000, 10000);
                string imgName = prefix + imgFile.FileName;

                string imgPath = Path.Combine("wwwroot/image/group/");
                if (!Directory.Exists(imgPath))
                    Directory.CreateDirectory(imgPath);

                string savePath = Path.Combine(imgPath, imgName);
                using (Stream imgStream = new FileStream(savePath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(imgStream);
                }

                @group.Img = imgName;
            }

            _context.Add(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(@group);
    }

    // GET: Admin/Groups/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Groups == null)
        {
            return NotFound();
        }

        var @group = await _context.Groups.FindAsync(id);
        if (@group == null)
        {
            return NotFound();
        }
        return View(@group);
    }

    // POST: Admin/Groups/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Group @group, IFormFile imgFile)
    {
        if (id != @group.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (imgFile != null)
                {
                    //save new image
                    Random r = new Random();
                    int prefix = r.Next(1000, 10000);
                    string imgName = prefix + imgFile.FileName;

                    string imgPath = Path.Combine("wwwroot/image/group/");
                    if (!Directory.Exists(imgPath))
                        Directory.CreateDirectory(imgPath);

                    string savePath = Path.Combine(imgPath, imgName);
                    using (Stream imgStream = new FileStream(savePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(imgStream);
                    }

                    //delete old image
                    if (System.IO.File.Exists(imgPath + group.Img))
                    {
                        System.IO.File.Delete(imgPath + group.Img);
                    }

                    //save new image name in database model
                    @group.Img = imgName;
                }

                _context.Update(@group);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(@group.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(@group);
    }

    // GET: Admin/Groups/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Groups == null)
        {
            return NotFound();
        }

        var @group = await _context.Groups
            .FirstOrDefaultAsync(m => m.Id == id);
        if (@group == null)
        {
            return NotFound();
        }

        return View(@group);
    }

    // POST: Admin/Groups/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Groups == null)
        {
            return Problem("Entity set 'DatabaseContext.Groups'  is null.");
        }
        var @group = await _context.Groups.FindAsync(id);
        if (@group != null)
        {

            string imgPath = Path.Combine("wwwroot/image/group/");

            //delete old image
            if (System.IO.File.Exists(imgPath + group.Img))
            {
                System.IO.File.Delete(imgPath + group.Img);
            }

            _context.Groups.Remove(@group);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupExists(int id)
    {
        return _context.Groups.Any(e => e.Id == id);
    }
}
