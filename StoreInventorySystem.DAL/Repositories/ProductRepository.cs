using StoreInventorySystem.DAL.Entities;
using StoreInventorySystem.DAL.Interfaces;

namespace StoreInventorySystem.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _filePath;

        public ProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(Product product)
        {
            using (var sw = new StreamWriter(_filePath, true))
            {
                sw.WriteLine($"{product.Id}|{product.Name}|{product.Price}|{product.StockCount}|{product.CategoryId}|{product.IsActive}");
            }
        }

        public List<Product> GetAll()
        {
            var list = new List<Product>();

            if (!File.Exists(_filePath))
            {
                return list;
            }

            using (var sr = new StreamReader(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split('|');

                    list.Add(new Product
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Price = decimal.Parse(parts[2]),
                        StockCount = int.Parse(parts[3]),
                        CategoryId = int.Parse(parts[4]),
                        IsActive = bool.Parse(parts[5])
                    });
                }
            }

            return list;
        }

        public void Update(List<Product> products)
        {
            using (var sw = new StreamWriter(_filePath, false))
            {
                foreach (var p in products)
                {
                    sw.WriteLine($"{p.Id}|{p.Name}|{p.Price}|{p.StockCount}|{p.CategoryId}|{p.IsActive}");
                }
            }
        }
    }
}
