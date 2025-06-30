using DeliveryManagement.Data;
using DeliveryManagement.Models;
using DeliveryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DeliveryManagement.Controllers
{
    [Route("[controller]")]
    public class CommandsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PdfService _pdfService;

        public CommandsController(ApplicationDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // GET: /Commands
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var commands = await _context.Commands
                .Include(c => c.Deliverer)
                .ToListAsync();

            return View(commands);
        }

        // GET: /Commands/Details/{id}
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var command = await _context.Commands
                .Include(c => c.Deliverer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (command == null)
                return NotFound();

            return View(command);
        }

        // GET: /Commands/PrintReceipt/{id}
        [HttpGet("PrintReceipt/{id}")]
        public async Task<IActionResult> PrintReceipt(int id)
        {
            var command = await _context.Commands
                .Include(c => c.Deliverer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (command == null)
                return NotFound();

            var model = new ReceiptViewModel
            {
                OrderId = command.Id,
                CustomerName = command.CustomerName,
                Address = command.Address,
                Product = command.Product,
                DeliveryDate = command.Date,
                DelivererName = command.Deliverer?.FullName ?? "Non assigné",
                DelivererPhone = command.Deliverer?.PhoneNumber ?? "N/A",
                CompanyName = "DeliveryManagement",
                PrintDate = DateTime.Now
            };

            var pdfBytes = _pdfService.GenerateReceiptPdf(model);
            return File(pdfBytes, "application/pdf", $"DeliveryReceipt_{command.Id}.pdf");
        }

        // GET: /Commands/Edit/{id}
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var command = await _context.Commands.FindAsync(id);
            if (command == null)
                return NotFound();

            ViewData["DelivererId"] = new SelectList(_context.Deliverers, "Id", "FullName", command.DelivererId);
            return View(command);
        }

        // POST: /Commands/Edit/{id}
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,Address,Product,Date,DelivererId")] Command command)
        {
            if (id != command.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(command);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Commands.Any(e => e.Id == command.Id))
                        return NotFound();
                    throw;
                }
            }

            ViewData["DelivererId"] = new SelectList(_context.Deliverers, "Id", "FullName", command.DelivererId);
            return View(command);
        }

        // GET: /Commands/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["DelivererId"] = new SelectList(_context.Deliverers, "Id", "FullName");
            return View();
        }

        // POST: /Commands/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerName,Address,Product,Date,DelivererId")] Command command)
        {
            if (ModelState.IsValid)
            {
                _context.Commands.Add(command);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DelivererId"] = new SelectList(_context.Deliverers, "Id", "FullName", command.DelivererId);
            return View(command);
        }
        // GET: /Commands/Delete/{id}
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = await _context.Commands
                .Include(c => c.Deliverer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (command == null)
                return NotFound();

            return View(command);
        }

        // POST: /Commands/Delete/{id}
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = await _context.Commands.FindAsync(id);
            if (command == null)
                return NotFound();

            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
