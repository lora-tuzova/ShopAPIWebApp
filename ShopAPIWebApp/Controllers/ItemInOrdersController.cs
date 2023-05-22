using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPIWebApp.Models;

namespace ShopAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInOrdersController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public ItemInOrdersController(ShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/ItemInOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemInOrder>>> GetItemsInOrder()
        {
          if (_context.ItemsInOrder == null)
          {
              return NotFound();
          }
            return await _context.ItemsInOrder.ToListAsync();
        }

        // GET: api/ItemInOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemInOrder>> GetItemInOrder(int id)
        {
          if (_context.ItemsInOrder == null)
          {
              return NotFound();
          }
            var itemInOrder = await _context.ItemsInOrder.FindAsync(id);

            if (itemInOrder == null)
            {
                return NotFound();
            }

            return itemInOrder;
        }

        // PUT: api/ItemInOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemInOrder(int id, ItemInOrder itemInOrder)
        {
            if (id != itemInOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemInOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemInOrderExists(id))
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

        // POST: api/ItemInOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemInOrder>> PostItemInOrder(ItemInOrder itemInOrder)
        {
          if (_context.ItemsInOrder == null)
          {
              return Problem("Entity set 'ShopAPIContext.ItemsInOrder'  is null.");
          }
            _context.ItemsInOrder.Add(itemInOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemInOrder", new { id = itemInOrder.Id }, itemInOrder);
        }

        // DELETE: api/ItemInOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemInOrder(int id)
        {
            if (_context.ItemsInOrder == null)
            {
                return NotFound();
            }
            var itemInOrder = await _context.ItemsInOrder.FindAsync(id);
            if (itemInOrder == null)
            {
                return NotFound();
            }

            _context.ItemsInOrder.Remove(itemInOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemInOrderExists(int id)
        {
            return (_context.ItemsInOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
