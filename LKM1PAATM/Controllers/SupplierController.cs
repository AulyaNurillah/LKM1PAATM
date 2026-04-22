using Microsoft.AspNetCore.Mvc;
using Npgsql;
using LKM1PAATM.Data;
using LKM1PAATM.Models;

namespace LKM1PAATM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly AppDbConnection _db;

        public SupplierController(AppDbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = new List<object>();

            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("select * from suppliers order by id asc", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new
                {
                    id = reader["id"],
                    nama = reader["nama"],
                    telepon = reader["telepon"],
                    alamat = reader["alamat"]
                });
            }

            return Ok(new
            {
                status = "success",
                data = list
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Suppliers supplier)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("insert into supplier (nama, telepon, alamat) values (@nama, @telepon, @alamat)", conn);
            cmd.Parameters.AddWithValue("nama", supplier.Nama);
            cmd.Parameters.AddWithValue("telepon", supplier.Telepon);
            cmd.Parameters.AddWithValue("alamat", supplier.Alamat);
            cmd.ExecuteNonQuery();
            return Ok(new
            {
                status = "success",
                message = "Supplier berhasil ditambahkan"
            });
        }
    }
}
