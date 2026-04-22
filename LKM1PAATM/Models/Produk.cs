namespace LKM1PAATM.Models
{
    public class Produk
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public int KategoriId { get; set; }
        public int SupplierId { get; set; }
    }
}
