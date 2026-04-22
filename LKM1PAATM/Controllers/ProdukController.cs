using LKM1PAATM.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data.Common;
using LKM1PAATM.Data;

[ApiController]
[Route("api/[controller]")]
public class ProdukController : Controller
{
    private readonly AppDbConnection _db;

    public ProdukController(AppDbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var list = new List<object>();

        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT * FROM produk ORDER BY id ASC", conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new
            {
                id = reader["id"],
                nama = reader["nama"],
                harga = reader["harga"],
                stok = reader["stok"],
                kategori_id = reader["kategori_id"],
                supplier_id = reader["supplier_id"]
            });
        }

        return Ok(new
        {
            status = "success",
            data = list
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT * FROM produk WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        var reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            return NotFound(new
            {
                status = "error",
                message = "Product tidak ditemukan"
            });
        }

        var data = new
        {
            id = reader["id"],
            nama = reader["nama"],
            harga = reader["harga"],
            stok = reader["stok"],
            kategori_id = reader["kategori_id"],
            supplier_id = reader["supplier_id"]
        };

        return Ok(new
        {
            status = "success",
            data = data
        });
    }

    [HttpPost]
    public IActionResult Create([FromBody] Produk produk)
    {
        try
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand(@"
                INSERT INTO produk (nama, harga, stok, kategori_id, supplier_id)
                VALUES (@nama, @harga, @stok, @kategori_id, @supplier_id)
            ", conn);

            cmd.Parameters.AddWithValue("nama", produk.Nama);
            cmd.Parameters.AddWithValue("harga", produk.Harga);
            cmd.Parameters.AddWithValue("stok", produk.Stok);
            cmd.Parameters.AddWithValue("kategori_id", produk.KategoriId);
            cmd.Parameters.AddWithValue("supplier_id", produk.SupplierId);
            cmd.ExecuteNonQuery();

            return Ok(new
            {
                status = "success",
                message = "Product berhasil ditambahkan"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "error",
                message = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Produk produk)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var checkCmd = new NpgsqlCommand("SELECT * FROM produk WHERE id=@id", conn);
        checkCmd.Parameters.AddWithValue("id", id);

        var reader = checkCmd.ExecuteReader();

        if (!reader.Read())
        {
            return NotFound(new
            {
                status = "error",
                message = "Product tidak ditemukan"
            });
        }

        reader.Close();

        var cmd = new NpgsqlCommand(@"
            UPDATE produk 
            SET nama=@nama, harga=@harga, stok=@stok,
                kategori_id=@kategori_id, supplier_id=@supplier_id,
                updated_at = CURRENT_TIMESTAMP
            WHERE id=@id
        ", conn);

        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("nama", produk.Nama);
        cmd.Parameters.AddWithValue("harga", produk.Harga);
        cmd.Parameters.AddWithValue("stok", produk.Stok);
        cmd.Parameters.AddWithValue("kategori_id", produk.KategoriId);
        cmd.Parameters.AddWithValue("supplier_id", produk.SupplierId);

        cmd.ExecuteNonQuery();

        return Ok(new
        {
            status = "success",
            message = "Product berhasil diupdate"
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new NpgsqlCommand("DELETE FROM produk WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        int affected = cmd.ExecuteNonQuery();

        if (affected == 0)
        {
            return NotFound(new
            {
                status = "error",
                message = "Product tidak ditemukan"
            });
        }

        return Ok(new
        {
            status = "success",
            message = "Product berhasil dihapus"
        });
    }
}