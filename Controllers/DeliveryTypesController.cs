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
    public class DeliveryTypesController : Controller
    {
        private readonly EntityContext _context;

        public DeliveryTypesController(EntityContext context)
        {
            _context = context;
        }

        // GET: DeliveryTypes
        public async Task<IActionResult> Index()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View(await _context.DeliveryType.ToListAsync());
        }

        // GET: DeliveryTypes/Details/5
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

            var deliveryType = await _context.DeliveryType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // GET: DeliveryTypes/Create
        public IActionResult Create()
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            return View();
        }

        // POST: DeliveryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type")] DeliveryType deliveryType)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (ModelState.IsValid)
            {
                _context.Add(deliveryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Edit/5
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

            var deliveryType = await _context.DeliveryType.FindAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }
            return View(deliveryType);
        }

        // POST: DeliveryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type")] DeliveryType deliveryType)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            if (id != deliveryType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryTypeExists(deliveryType.ID))
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
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Delete/5
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

            var deliveryType = await _context.DeliveryType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // POST: DeliveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(User.Identity.GetUserId() == "e63830c1-9176-4b57-9946-c91277275e40"))
            {
                Response.Redirect("/Home/Permissions");
            }

            var deliveryType = await _context.DeliveryType.FindAsync(id);
            _context.DeliveryType.Remove(deliveryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryTypeExists(int id)
        {
            return _context.DeliveryType.Any(e => e.ID == id);
        }
    }
}
