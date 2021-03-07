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
    public class ProductsController : Controller
    {
        private readonly EntityContext _context;

        public ProductsController(EntityContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Shop()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Permissions");
            }

            ViewBag.Category = "Most popular products";
            
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult ShopCategory(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Permissions");
            }

            switch (id)
            {
                case 0:
                    ViewBag.Category = "Other products";
                    break;
                case 1:
                    ViewBag.Category = "Cards";
                    break;
                case 2:
                    ViewBag.Category = "Sealed products";
                    break;
                case 3:
                    ViewBag.Category = "Accesory";
                    break;
                default:
                    return NotFound();
            }

            return View("Shop",_context.Products.Where(p => p.ID_ProductType == id));
        }

        public async Task<IActionResult> AddToCart(int? id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Permissions");
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

            CartItem item = new CartItem();

            item.CreatedTime = DateTime.Now;

            item.ID_Cart = cart.ID;

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            item.Product = product;

            item.ID_Product = product.ID;

            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Shop));
        }

        // GET: Products/Details/5
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Img,Price,ID_ProductType")] Product product)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
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

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Img,Price,ID_ProductType")] Product product)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
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
            return View(product);
        }

        // GET: Products/Delete/5
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
