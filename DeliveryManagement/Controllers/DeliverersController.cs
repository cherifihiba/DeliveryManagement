using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryManagement.Data;
using DeliveryManagement.Models;

namespace DeliveryManagement.Controllers
{
    public class DeliverersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliverersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deliverers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deliverers.ToListAsync());
        }

        // GET: Deliverers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverer = await _context.Deliverers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliverer == null)
            {
                return NotFound();
            }

            return View(deliverer);
        }

        // GET: Deliverers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deliverers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,PhoneNumber")] Deliverer deliverer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliverer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliverer);
        }

        // GET: Deliverers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverer = await _context.Deliverers.FindAsync(id);
            if (deliverer == null)
            {
                return NotFound();
            }
            return View(deliverer);
        }

        // POST: Deliverers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,PhoneNumber")] Deliverer deliverer)
        {
            if (id != deliverer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliverer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelivererExists(deliverer.Id))
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
            return View(deliverer);
        }

        // GET: Deliverers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverer = await _context.Deliverers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliverer == null)
            {
                return NotFound();
            }

            return View(deliverer);
        }

        // POST: Deliverers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliverer = await _context.Deliverers.FindAsync(id);
            if (deliverer != null)
            {
                _context.Deliverers.Remove(deliverer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelivererExists(int id)
        {
            return _context.Deliverers.Any(e => e.Id == id);
        }
    }
}
