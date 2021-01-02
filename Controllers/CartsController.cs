using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCGShop;
using TCGShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace TCGShop.Controllers
{
    public class CartsController : Controller
    {
        private readonly EntityContext _context;

        public CartsController(EntityContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }
            return View(await _context.Cart.ToListAsync());
        }

        public async Task<IActionResult> Details()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Permissions");
            }
            
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                ViewBag.IsAdmin = false;
            }
            else
            {
                ViewBag.IsAdmin = true;
            }

            var userId = User.Identity.GetUserId();

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.ID_Customer == userId);

            if (cart == null)
            {
                cart = new Cart() { ID_Customer = userId, CreatedTime = DateTime.Now };
                _context.Cart.Add(cart);
                _context.SaveChanges();
            }

            int id = cart.ID;

            List<Product> temp = new List<Product>();

            foreach (var item in _context.CartItem.Where(p => p.ID_Cart == id).ToList())
            {
                Product prod = _context.Products
                    .Where(b => b.ID == item.ID_Product)
                    .FirstOrDefault();

                temp.Add(prod);
            }

            ViewData["PIL"] = temp;

            return View(cart);

        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreatedTime,ID_Customer")] Cart cart)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedTime,ID_Customer")] Cart cart)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id != cart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.ID))
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
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.ID == id);
        }
    }
}
