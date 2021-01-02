using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCGShop;
using TCGShop.Models;

namespace TCGShop.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly EntityContext _context;

        public CartItemsController(EntityContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }
            return View(await _context.CartItem.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreatedTime,Quantity,ID_Cart,ID_Product")] CartItem cartItem)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
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

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedTime,Quantity,ID_Cart,ID_Product")] CartItem cartItem)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id != cartItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.ID))
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
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
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

            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.ID == id);
        }
    }
}
