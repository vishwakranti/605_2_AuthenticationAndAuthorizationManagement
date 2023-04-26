using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeSparePartsShop.Data;
using BikeSparePartsShop.Models;

namespace BikeSparePartsShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StocksApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StocksApiController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStock()
        {
          if (_context.Stock == null)
          {
              return NotFound();
          }
            return await _context.Stock.ToListAsync();
        }

        // GET: api/StocksApiController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(Guid id)
        {
          if (_context.Stock == null)
          {
              return NotFound();
          }
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/StocksApiController/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(Guid id, Stock stock)
        {
            if (id != stock.StockId)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StocksApiController
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
          if (_context.Stock == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Stock'  is null.");
          }
            _context.Stock.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock", new { id = stock.StockId }, stock);
        }

        // DELETE: api/StocksApiController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            if (_context.Stock == null)
            {
                return NotFound();
            }
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExists(Guid id)
        {
            return (_context.Stock?.Any(e => e.StockId == id)).GetValueOrDefault();
        }
    }
}
