using Microsoft.AspNetCore.Mvc;
using Npgsql;
using LKM1PAATM.Data;
using LKM1PAATM.Models;

namespace LKM1PAATM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategoriController : Controller
    {
        private readonly AppDbConnection _db;

        public KategoriController(AppDbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GettAll()
        {
            var list = new List<object>();

            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("select * from kategori order by id asc", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new
                {
                    id = reader["id"],
                    nama = reader["nama"]
                });
            }

            return Ok(new
            {
                status = "success",
                data = list
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Kategori kategori)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("insert into kategori (nama) values (@nama)", conn);
            cmd.Parameters.AddWithValue("nama", kategori.Nama);
            cmd.ExecuteNonQuery();
            return Ok(new
            {
                status = "success",
                message = "Kategori berhasil ditambahkan"
            });
        }
    }
}
