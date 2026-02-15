using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuangKuApi.Data;
using RuangKuApi.Models;

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

    // lihat data
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Peminjaman>>> GetAll()
    {
        return await _context.Peminjamans.ToListAsync();
    }

    // detail
    [HttpGet("{id}")]
    public async Task<ActionResult<Peminjaman>> GetById(int id)
    {
        var data = await _context.Peminjamans.FindAsync(id);
        if (data == null) return NotFound();
        return data;
    }

    // tambah data
    [HttpPost]
    public async Task<ActionResult<Peminjaman>> Create(Peminjaman peminjaman)
    {
        _context.Peminjamans.Add(peminjaman);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = peminjaman.Id }, peminjaman);
    }

    // update
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Peminjaman peminjaman)
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

    // delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await _context.Peminjamans.FindAsync(id);
        if (data == null) return NotFound();

        _context.Peminjamans.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}