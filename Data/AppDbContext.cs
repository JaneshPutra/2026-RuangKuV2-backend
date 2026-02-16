using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RuangKuApi.Models;

namespace RuangKuApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Peminjaman> Peminjamans { get; set; } = null!;
    public DbSet<Ruangan> Ruangan { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=ruangku.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}