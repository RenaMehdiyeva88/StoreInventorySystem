using StoreInventorySystem.DAL.Entities;
using StoreInventorySystem.DAL.Interfaces;

namespace StoreInventorySystem.BLL.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public void AddCategory(string name)
        {
            var categories = _repo.GetAll();

            if (categories.Any(c => c.IsActive &&
                                    c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("A category with this name already exists!");
            }

            int newId = categories.Any() ? categories.Max(c => c.Id) + 1 : 1;

            _repo.Add(new Category
            {
                Id = newId,
                Name = name,
                IsActive = true
            });
        }

        public List<Category> GetAllCategories()
        {
            return _repo.GetAll().Where(c => c.IsActive).ToList();
        }
    }
}
