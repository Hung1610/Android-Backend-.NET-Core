using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly RestaurantAppContext _context;

        public FoodsController(RestaurantAppContext context)
        {
            _context = context;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Foods>>> GetFoods()
        {
            return await _context.Foods.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Foods>> GetFoods(long id)
        {
            var foods = await _context.Foods.FindAsync(id);

            if (foods == null)
            {
                return NotFound();
            }

            return foods;
        }

        // PUT: api/Foods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoods(long id, Foods foods)
        {
            if (id != foods.Id)
            {
                return BadRequest();
            }

            _context.Entry(foods).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodsExists(id))
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

        // POST: api/Foods
        [HttpPost]
        public async Task<ActionResult<Foods>> PostFoods(Foods foods)
        {
            _context.Foods.Add(foods);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodsExists(foods.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFoods", new { id = foods.Id }, foods);
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Foods>> DeleteFoods(long id)
        {
            var foods = await _context.Foods.FindAsync(id);
            if (foods == null)
            {
                return NotFound();
            }

            _context.Foods.Remove(foods);
            await _context.SaveChangesAsync();

            return foods;
        }

        private bool FoodsExists(long id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
