using LKM1PAATM.Data;
using LKM1PAATM.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace LKM1PAATM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaksiController : Controller
    {
        private readonly AppDbConnection _db;

        public TransaksiController(AppDbConnection db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TransactionRequest request)
        {
            if (request.Details == null || request.Details.Count == 0)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Detail transaksi tidak boleh kosong"
                });
            }

            using var conn = _db.GetConnection();
            conn.Open();

            using var trans = conn.BeginTransaction();

            try
            {
                decimal totalPrice = 0;

                foreach (var item in request.Details)
                {
                    var cmd = new NpgsqlCommand(
                        "select harga, stok from produk where id=@id",
                        conn, trans
                    );

                    cmd.Parameters.AddWithValue("id", item.ProductId);

                    var reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        return BadRequest(new
                        {
                            status = "error",
                            message = $"Produk ID {item.ProductId} tidak ditemukan"
                        });
                    }

                    decimal price = (decimal)reader["harga"];
                    int stock = (int)reader["stok"];

                    reader.Close();

                    if (stock < item.Quantity)
                    {
                        return BadRequest(new
                        {
                            status = "error",
                            message = $"Stok tidak cukup untuk produk ID {item.ProductId}"
                        });
                    }

                    totalPrice += price * item.Quantity;
                }

                var cmdTrans = new NpgsqlCommand(
                    "insert into transaksi (total_harga) values (@total) returning id",
                    conn, trans
                );

                cmdTrans.Parameters.AddWithValue("total", totalPrice);
                int transactionId = (int)cmdTrans.ExecuteScalar();

                foreach (var item in request.Details)
                {
                    var cmdPrice = new NpgsqlCommand(
                        "select harga from produk where id=@id",
                        conn, trans
                    );

                    cmdPrice.Parameters.AddWithValue("id", item.ProductId);
                    decimal price = (decimal)cmdPrice.ExecuteScalar();

                    decimal subtotal = price * item.Quantity;

                    var cmdDetail = new NpgsqlCommand(@"
                    insert into detail_transaksi 
                    (transaksi_id, produk_id, jumlah, subtotal)
                    values (@tid, @pid, @jml, @sub)
                ", conn, trans);

                    cmdDetail.Parameters.AddWithValue("tid", transactionId);
                    cmdDetail.Parameters.AddWithValue("pid", item.ProductId);
                    cmdDetail.Parameters.AddWithValue("jml", item.Quantity);
                    cmdDetail.Parameters.AddWithValue("sub", subtotal);

                    cmdDetail.ExecuteNonQuery();

                    var cmdStock = new NpgsqlCommand(@"
                    update produk
                    set stok = stok - @jml
                    where id=@id
                ", conn, trans);

                    cmdStock.Parameters.AddWithValue("jml", item.Quantity);
                    cmdStock.Parameters.AddWithValue("id", item.ProductId);

                    cmdStock.ExecuteNonQuery();
                }

                trans.Commit();

                return Ok(new
                {
                    status = "success",
                    message = "Transaksi berhasil",
                    transaction_id = transactionId,
                    total = totalPrice
                });
            }
            catch (Exception ex)
            {
                trans.Rollback();

                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }
    }
}
