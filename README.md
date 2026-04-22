Kasir API Sederhana

Project ini merupakan aplikasi backend berbasis API yang dirancang untuk mensimulasikan sistem kasir sederhana (point of sale).
Aplikasi ini digunakan untuk mengelola data produk, kategori, supplier, serta menangani proses transaksi penjualan secara otomatis.

Fitur utama yang tersedia:
a. Pengelolaan data produk (CRUD)
b. Pengelolaan kategori dan supplier
c. Pembuatan transaksi penjualan
d. Perhitungan total transaksi secara otomatis
e. Penyimpanan detail setiap transaksi
f. Pengurangan stok produk secara otomatis setelah transaksi

Teknologi yang Digunakan
Bahasa Pemrograman C#
Framework ASP.NET Core Web API
Database PostgreSQL
Tools Pendukung : Visual Studio, pgAdmin, Swagger

Cara Instalasi dan Menjalankan Project
1. Clone Repository
bash
git clone https://github.com/AulyaNurillah/LKM1PAATM.git
cd kasir-api

2. Membuka Project
Jalankan Visual Studio
Pilih menu Open Project
Buka file dengan ekstensi 

3. Install Dependency
Jika diperlukan, jalankan:
bash
dotnet restore

4. Konfigurasi Koneksi Database
Sesuaikan file appsettings.json dengan konfigurasi database lokal:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=kasir_db;Username=postgres;Password=YOUR_PASSWORD"
}

Cara Import Database
1. Buka aplikasi pgAdmin
2. Buat database baru dengan nama:
   kasir_db
3. Buka Query Tool
4. Salin isi file database.sql
5. Jalankan query tersebut hingga selesai

Menjalankan Aplikasi
Melalui Visual Studio:
Klik tombol Run / IIS Express
Atau melalui terminal:
bash
dotnet run

Daftar Endpoint API
| Method | URL                | Deskripsi                                |
| ------ | ------------------ | ---------------------------------------- |
| GET    | /api/products      | Menampilkan seluruh data produk          |
| GET    | /api/products/{id} | Menampilkan detail produk berdasarkan ID |
| POST   | /api/products      | Menambahkan data produk baru             |
| PUT    | /api/products/{id} | Memperbarui data produk                  |
| DELETE | /api/products/{id} | Menghapus data produk                    |
| GET    | /api/categories    | Menampilkan seluruh kategori             |
| POST   | /api/categories    | Menambahkan kategori baru                |
| GET    | /api/suppliers     | Menampilkan seluruh supplier             |
| POST   | /api/suppliers     | Menambahkan supplier baru                |
| POST   | /api/transactions  | Membuat transaksi penjualan              |
| GET    | /api/transactions  | Menampilkan riwayat transaksi            |

Link Video Presentasi

Link video presentasi:
https://youtu.be/7MvRY791Jx4
