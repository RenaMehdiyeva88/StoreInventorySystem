namespace StoreInventorySystem.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;


        public override string ToString()
        {
            return $"{Id} | {Name} | Price: {Price:C} | Stock: {StockCount} | CategoryId: {CategoryId} | {(IsActive ? "Active" : "Inactive")}";
        }
    }
}

