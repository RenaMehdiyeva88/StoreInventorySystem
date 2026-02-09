using StoreInventorySystem.DAL.Entities;

namespace StoreInventorySystem.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        List<Category> GetAll();
    }
}

