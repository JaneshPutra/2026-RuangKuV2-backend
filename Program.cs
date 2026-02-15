using Microsoft.EntityFrameworkCore;
using RuangKuApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Tambahkan servis Controller agar API bisa jalan
builder.Services.AddControllers();

// 2. Konfigurasi Swagger/OpenAPI (Biar bisa ngetes API nanti)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. DAFTARKAN DATABASE (Ini yang tadi hilang!)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 4. Mapping Controller
app.MapControllers();

app.Run();