using System;
using System.Collections;
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
    public class ItemsController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public ItemsController(ShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(byte id)
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(byte id, Item item)
        {
            if (id != item.ItemID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                var itOrders = _context.ItemsInOrder.Where(i=>i.IdItem == id).Include(i=>i.Order);
                foreach (var itOrder in itOrders)
                {
                    if (itOrder.Order.OrderStatus == 0)
                    {
                        itOrder.Order.OrderPrice = 0;
                        _context.Items.Load();
                        foreach (ItemInOrder i in itOrder.Order.OrderItems)
                        {
                            itOrder.Order.OrderPrice += i.Item.ItemPrice * i.ItemQuantity;
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
          if (_context.Items == null)
          {
              return Problem("Entity set 'ShopAPIContext.Items'  is null.");
          }
            _context.Items.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.ItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItem", new { id = item.ItemID }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(byte id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            if (item.ItemStatus == 1)
            {
                item.ItemStatus = 0;
                _context.Orders.Load();
                var itOrders = _context.ItemsInOrder.Where(o => o.IdItem == id);
                foreach (var i in itOrders)
                {
                    if (i.Order.OrderStatus==0) _context.ItemsInOrder.Remove(i);
                }
            }
            else item.ItemStatus = 1;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(byte id)
        {
            return (_context.Items?.Any(e => e.ItemID == id)).GetValueOrDefault();
        }
    }
}
