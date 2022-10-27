using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeSparePartsShop.Data;
using BikeSparePartsShop.Models;

namespace BikeSparePartsShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Order.Include(c => c.Customer).Include(c => c.Staff)
                                             .Include(c => c.Stock)
                                              
                                              .ToListAsync();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(c => c.Stock)
                                            .Include(c => c.Customer)
                                            .Include(c => c.Staff)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData[index: "StaffId"] = new SelectList(items: _context.Staff, dataValueField: "StaffId", dataTextField: "Name");
            ViewData[index: "CustomerId"] = new SelectList(items: _context.Customer, dataValueField: "CustomerId", dataTextField: "Name");
            ViewData[index: "StockId"] = new SelectList(items: _context.Stock, dataValueField: "StockId", dataTextField: "ProductName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,ShippedDate,CustomerId,StockId,StaffId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderId = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData[index: "StaffId"] = new SelectList(items: _context.Staff, dataValueField: "StaffId", dataTextField: "Name");
            ViewData[index: "CustomerId"] = new SelectList(items: _context.Customer, dataValueField: "CustomerId", dataTextField: "Name");
            ViewData[index: "StockId"] = new SelectList(items: _context.Stock, dataValueField: "StockId", dataTextField: "ProductName");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,OrderDate,ShippedDate,CustomerId,StockId,StaffId")] Order order)
        {
            if (id != order.OrderId)
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
                    if (!OrderExists(order.OrderId))
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
            ViewData[index: "StaffId"] = new SelectList(items: _context.Staff, dataValueField: "StaffId", dataTextField: "Name");
            ViewData[index: "CustomerId"] = new SelectList(items: _context.Customer, dataValueField: "CustomerId", dataTextField: "Name");
            ViewData[index: "StockId"] = new SelectList(items: _context.Stock, dataValueField: "StockId", dataTextField: "ProductName");
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
          return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
