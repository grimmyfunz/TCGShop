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

namespace TCGShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EntityContext _context;

        public OrdersController(EntityContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Permissions");
            }

            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                //Response.Redirect("/Home/Permissions");
                string id = User.Identity.GetUserId();
                ViewBag.IsAdmin = false;
                return View(_context.Order.Where(m => m.ID_User == id));
            }
            ViewBag.IsAdmin = true;
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
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

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            List<Product> temp = new List<Product>();

            foreach (var item in _context.CartItem.Where(p => p.ID_Cart == order.ID_Cart).ToList())
            {
                Product prod = _context.Products
                    .Where(b => b.ID == item.ID_Product)
                    .FirstOrDefault();

                temp.Add(prod);
            }

            ViewData["PIL"] = temp;

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreatedTime,ID_DeliveryType,Username,ID_User")] Order order)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> MakeOrder()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            Order order = new Order();
            order.CreatedTime = DateTime.Now;
            order.ID_User = User.Identity.GetUserId();
            order.Username = User.Identity.GetUserName();

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.ID_Customer == order.ID_User);

            order.Cart = cart;
            order.ID_Cart = cart.ID;
            order.Cart.ID_Customer = "Shopping cart is ordered";
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Edit/5
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

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedTime,ID_DeliveryType,Username,ID_User")] Order order)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            return View(order);
        }

        // GET: Orders/Delete/5
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

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
    }
}
