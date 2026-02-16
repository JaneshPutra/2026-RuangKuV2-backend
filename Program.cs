using Microsoft.EntityFrameworkCore;
using RuangKuApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Tambahkan servis Controller
builder.Services.AddControllers();

// 2. Konfigurasi Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. DAFTARKAN DATABASE SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. KONFIGURASI CORS (Solusi Error Akses dari React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Alamat frontend kamu
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5. AKTIFKAN CORS (Wajib diletakkan sebelum MapControllers)
app.UseCors("AllowReactApp");

// Nonaktifkan HttpsRedirection sementara jika kamu sering bermasalah dengan sertifikat SSL di localhost
// app.UseHttpsRedirection();

app.UseAuthorization();

// 6. Mapping Controller
app.MapControllers();

app.Run();