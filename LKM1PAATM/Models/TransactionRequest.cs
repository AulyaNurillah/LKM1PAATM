namespace LKM1PAATM.Models
{
    public class TransactionRequest
    {
        public List<TransactionItem> Details { get; set; } = new();
    }

    public class TransactionItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
