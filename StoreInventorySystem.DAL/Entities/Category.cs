namespace StoreInventorySystem.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;


        public override string ToString()
        {
            return $"{Id} | {Name} | {(IsActive ? "Active" : "Inactive")}";
        }
    }
}

