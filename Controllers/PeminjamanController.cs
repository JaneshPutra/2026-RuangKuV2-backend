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

    // --- GET: Fetch Data dengan Search & Filter ---
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Peminjaman>>> GetPeminjaman(
        [FromQuery] string? search,
        [FromQuery] string? status)
    {
        var query = _context.Peminjamans.AsQueryable();

        // 1. Filter Pencarian
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p =>
                p.NamaPeminjam.ToLower().Contains(search.ToLower()) ||
                p.Ruangan.ToLower().Contains(search.ToLower()));
        }

        // 2. Filter Status
        if (!string.IsNullOrEmpty(status) && status != "All")
        {
            query = query.Where(p => p.Status == status);
        }

        // 3. Sorting: Terbaru muncul paling atas
        return await query.OrderByDescending(p => p.Id).ToListAsync();
    }

    // --- GET BY ID ---
    [HttpGet("{id}")]
    public async Task<ActionResult<Peminjaman>> GetPeminjaman(int id)
    {
        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound();
        return peminjaman;
    }

    // --- POST: Tambah Peminjaman + Validasi Bentrok ---
    [HttpPost]
    public async Task<ActionResult<Peminjaman>> PostPeminjaman(Peminjaman peminjaman)
    {
        // Validasi: Cek apakah ruangan sudah terisi di tanggal tersebut
        var isConflict = await _context.Peminjamans.AnyAsync(p =>
            p.Ruangan == peminjaman.Ruangan &&
            p.TanggalPinjam.Date == peminjaman.TanggalPinjam.Date &&
            p.Status != "Ditolak");

        if (isConflict)
        {
            return BadRequest(new { message = "Gagal! Ruangan ini sudah dipesan pada tanggal tersebut." });
        }

        _context.Peminjamans.Add(peminjaman);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPeminjaman), new { id = peminjaman.Id }, peminjaman);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPeminjaman(int id, Peminjaman peminjaman)
    {
        if (id != peminjaman.Id) return BadRequest();

        // Validasi Edit: Cari apakah ada booking LAIN yang:
        // 1. Bukan data yang sedang diedit (p.Id != id)
        // 2. Ruangannya sama
        // 3. Tanggal, Bulan, dan Tahun-nya sama persis
        // 4. Statusnya sudah 'Disetujui' atau masih 'Menunggu' (Bukan Ditolak)
        var isConflict = await _context.Peminjamans.AnyAsync(p =>
            p.Id != id &&
            p.Ruangan == peminjaman.Ruangan &&
            p.TanggalPinjam.Year == peminjaman.TanggalPinjam.Year &&
            p.TanggalPinjam.Month == peminjaman.TanggalPinjam.Month &&
            p.TanggalPinjam.Day == peminjaman.TanggalPinjam.Day &&
            p.Status != "Ditolak");

        if (isConflict)
        {
            // Kita kirim pesan yang sangat jelas
            return BadRequest(new { message = $"Gagal! Ruangan {peminjaman.Ruangan} sudah terisi pada tanggal {peminjaman.TanggalPinjam:dd/MM/yyyy}." });
        }

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

    // --- DELETE ---
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