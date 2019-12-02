using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Dtos;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly RestaurantAppContext _context;

        public TablesController(RestaurantAppContext context)
        {
            _context = context;
        }


        [HttpPost("{tableId}/[action]")]
        public async Task<ActionResult<Bills>> UpdateOrders(long tableId, List<OrderDto> orders)
        {
            if (!TablesExists(tableId))
            {
                return NotFound();
            }

            var bill = await _context.Bills.Where(b => b.TableId == tableId && (b.Flag == 0 || b.Flag == null)).FirstOrDefaultAsync();
            if (bill == null)
            {
                return NotFound();
            }

            foreach (OrderDto orderDto in orders)
            {
                var order = new Orders()
                {
                    BillId = bill.Id,
                    FoodId = orderDto.FoodId,
                    Quantity = orderDto.Quantity,
                    PaymentAmount = orderDto.Quantity * ((long)await _context.Foods.Where(f => f.Id == orderDto.FoodId).Select(f=> f.Price).FirstOrDefaultAsync())
                };
                if (await _context.Orders.AnyAsync(o=> o.BillId == order.BillId && o.FoodId == order.FoodId))
                    _context.Entry(order).State = EntityState.Modified;
                else
                    await _context.Orders.AddAsync(order);
            }
            await _context.SaveChangesAsync();

            bill.TotalPayment = _context.Orders.Where(o => o.BillId == bill.Id).Sum(o => o.PaymentAmount);
            _context.Entry(bill).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return bill;
        }

        [HttpPost("{id}/[action]")]
        public async Task<ActionResult<Bills>> GetBilling(long id)
        {
            if (!TablesExists(id))
            {
                return NotFound();
            }

            var bill = await _context.Bills.Where(b => b.TableId == id && (b.Flag == 0 || b.Flag == null)).FirstOrDefaultAsync();
            var table = await _context.Tables.FindAsync(id);

            bill.Flag = 1;
            _context.Entry(bill).State = EntityState.Modified;
            table.Flag = 0;
            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return bill;
        }

        // GET: api/Tables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> GetTables()
        {
            return await _context.Tables.ToListAsync();
        }

        // GET: api/Tables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tables>> GetTables(long id)
        {
            var tables = await _context.Tables.FindAsync(id);

            if (tables == null)
            {
                return NotFound();
            }

            return tables;
        }

        // PUT: api/Tables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTables(long id, Tables tables)
        {
            if (id != tables.Id)
            {
                return BadRequest();
            }

            _context.Entry(tables).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablesExists(id))
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

        // POST: api/Tables
        [HttpPost]
        public async Task<ActionResult<Tables>> PostTables(Tables tables)
        {
            _context.Tables.Add(tables);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTables", new { id = tables.Id }, tables);
        }

        // DELETE: api/Tables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tables>> DeleteTables(long id)
        {
            var tables = await _context.Tables.FindAsync(id);
            if (tables == null)
            {
                return NotFound();
            }

            _context.Tables.Remove(tables);
            await _context.SaveChangesAsync();

            return tables;
        }

        private bool TablesExists(long id)
        {
            return _context.Tables.Any(e => e.Id == id);
        }
    }
}
