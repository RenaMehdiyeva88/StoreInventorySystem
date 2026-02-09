using StoreInventorySystem.DAL.Enums;

namespace StoreInventorySystem.DAL.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Id} | ProductId: {ProductId} | Quantity: {Quantity} | Type: {TransactionType} | Date: {Date}";
        }
    }
}

