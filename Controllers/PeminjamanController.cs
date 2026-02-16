using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuangKuApi.Models;
using RuangKuApi.Data;

namespace RuangKuApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeminjamanController : ControllerBase
{
    private readonly AppDbContext _context;

    public PeminjamanController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Peminjaman>>> GetPeminjaman(
        [FromQuery] string? search, 
        [FromQuery] string? status)
    {
        var query = _context.Peminjamans.AsQueryable(); 

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => 
                p.NamaPeminjam.ToLower().Contains(search.ToLower()) || 
                p.Ruangan.ToLower().Contains(search.ToLower()));
        }

        if (!string.IsNullOrEmpty(status) && status != "All")
        {
            query = query.Where(p => p.Status == status);
        }

        return await query.OrderByDescending(p => p.Id).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Peminjaman>> GetPeminjaman(int id)
    {
        var peminjaman = await _context.Peminjamans.FindAsync(id);

        if (peminjaman == null) return NotFound();

        return peminjaman;
    }

    [HttpPost]
    public async Task<ActionResult<Peminjaman>> PostPeminjaman(Peminjaman peminjaman)
    {
        _context.Peminjamans.Add(peminjaman);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPeminjaman), new { id = peminjaman.Id }, peminjaman);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPeminjaman(int id, Peminjaman peminjaman)
    {
        if (id != peminjaman.Id) return BadRequest();

        _context.Entry(peminjaman).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Peminjamans.Any(e => e.Id == id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePeminjaman(int id)
    {
        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound();

        _context.Peminjamans.Remove(peminjaman);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}