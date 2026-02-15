using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RuangKuApi.Models; // Pastikan ini sesuai nama project

namespace RuangKuApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Peminjaman> Peminjamans { get; set; } = null!;
}

// Tambahkan class ini tepat di bawahnya. Ini adalah "Jembatan Utama" untuk migrasi.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=ruangku.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}