using StoreInventorySystem.DAL.Entities;
namespace StoreInventorySystem.DAL.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
        List<Product> GetAll();
        void Update(List<Product> products);
    }
}

