using StoreInventorySystem.DAL.Entities;
using StoreInventorySystem.DAL.Interfaces;

namespace StoreInventorySystem.BLL.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductService(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public void AddProduct(string name, decimal price, int categoryId)
        {
            var categories = _categoryRepo.GetAll();
            var category = categories.FirstOrDefault(c => c.Id == categoryId && c.IsActive);

            if (category == null)
                throw new Exception("Category does not exist or is inactive!");

            var products = _productRepo.GetAll();

            if (products.Any(p => p.IsActive &&
                                  p.CategoryId == categoryId &&
                                  p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("There can't be a product with the same name in the same category!");
            }

            int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;

            _productRepo.Add(new Product
            {
                Id = newId,
                Name = name,
                Price = price,
                StockCount = 0,
                CategoryId = categoryId,
                IsActive = true
            });
        }

        public List<Product> GetAllProducts()
        {
            return _productRepo.GetAll().Where(p => p.IsActive).ToList();
        }
    }
}
