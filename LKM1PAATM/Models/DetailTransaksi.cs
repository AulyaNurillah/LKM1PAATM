namespace LKM1PAATM.Models
{
    public class DetailTransaksi
    {
        public int ID { get; set; }
        public int TransaksiId { get; set; }
        public int ProdukId { get; set; }
        public int Jumlah { get; set; }
        public decimal SubTotal { get; set; }
    }
}
