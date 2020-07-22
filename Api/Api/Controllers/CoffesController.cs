using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.BLL.Entity;
using Api.DAL.EF;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoffesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoffesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coffes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coffe>>> GetCoffes()
        {
            return await _context.Coffes.ToListAsync();
        }

        // GET: Coffes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coffe>> GetCoffe(int id)
        {
            var coffe = await _context.Coffes.FindAsync(id);

            if (coffe == null)
            {
                return NotFound();
            }

            return Ok(coffe);
        }

        // PUT: Coffes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffe(int id, Coffe coffe)
        {
            if (id != coffe.Id)
            {
                return BadRequest();
            }

            _context.Entry(coffe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeExists(id))
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

        // POST: Coffes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Coffe>> PostCoffe(Coffe coffe)
        {
            _context.Coffes.Add(coffe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoffe", new { id = coffe.Id }, coffe);
        }

        // DELETE: Coffes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Coffe>> DeleteCoffe(int id)
        {
            var coffe = await _context.Coffes.FindAsync(id);
            if (coffe == null)
            {
                return NotFound();
            }

            _context.Coffes.Remove(coffe);
            await _context.SaveChangesAsync();

            return Ok(coffe);
        }

        private bool CoffeExists(int id)
        {
            return _context.Coffes.Any(e => e.Id == id);
        }
    }
}
