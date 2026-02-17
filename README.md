# RuangKu - Backend API (ASP.NET Core)

Sistem Backend API untuk pengelolaan reservasi ruangan menggunakan ASP.NET Core dan SQLite.

## Fitur Utama
- **CRUD Peminjaman**: Pengelolaan data reservasi lengkap.
- **Master Data Ruangan**: Manajemen status ruangan (Available/Maintenance).
- **Conflict Validation**: Validasi cerdas untuk mencegah bentrok jadwal pada ruangan dan tanggal yang sama.
- **RESTful API**: Endpoint yang bersih dan terstruktur.

## Tech Stack
- **Framework**: .NET 8.0 / ASP.NET Core Web API
- **Database**: SQLite (Entity Framework Core)
- **Tools**: Swagger/OpenAPI untuk dokumentasi API.

## Cara Menjalankan
1. Pastikan .NET SDK sudah terinstall.
2. Clone repository.
3. Jalankan perintah restore:
   ```bash
   dotnet restore```
4. Update database 
   ```bash
   dotnet ef database update```
5. Jalankan Aplikasi
   ```bash
   dotnet run```
6. API akan berjalan di http://localhost:5205 dan Swagger dapat diakses di /swagger.
