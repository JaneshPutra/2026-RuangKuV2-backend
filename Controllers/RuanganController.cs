using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuangKuApi.Data; 
using RuangKuApi.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RuangKuApi.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuanganController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RuanganController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ruangan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruangan>>> GetRuangan()
        {
            return await _context.Ruangan.ToListAsync();
        }

        // GET: api/Ruangan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ruangan>> GetRuangan(int id)
        {
            var ruangan = await _context.Ruangan.FindAsync(id);

            if (ruangan == null)
            {
                return NotFound();
            }

            return ruangan;
        }

        // POST: api/Ruangan
        [HttpPost]
        public async Task<ActionResult<Ruangan>> PostRuangan(Ruangan ruangan)
        {
            _context.Ruangan.Add(ruangan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRuangan), new { id = ruangan.Id }, ruangan);
        }

        // PUT: api/Ruangan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuangan(int id, Ruangan ruangan)
        {
            if (id != ruangan.Id)
            {
                return BadRequest();
            }

            _context.Entry(ruangan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuanganExists(id))
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

        // DELETE: api/Ruangan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuangan(int id)
        {
            var ruangan = await _context.Ruangan.FindAsync(id);
            if (ruangan == null)
            {
                return NotFound();
            }

            _context.Ruangan.Remove(ruangan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuanganExists(int id)
        {
            return _context.Ruangan.Any(e => e.Id == id);
        }
    }
}