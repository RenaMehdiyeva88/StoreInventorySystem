using StoreInventorySystem.DAL.Entities;
using StoreInventorySystem.DAL.Interfaces;

namespace StoreInventorySystem.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _filePath;

        public CategoryRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(Category category)
        {
            using (var sw = new StreamWriter(_filePath, true))
            {
                sw.WriteLine($"{category.Id}|{category.Name}|{category.IsActive}");
            }
        }

        public List<Category> GetAll()
        {
            var list = new List<Category>();

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

                    list.Add(new Category
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        IsActive = bool.Parse(parts[2])
                    });
                }
            }

            return list;
        }
    }
}
