using StoreInventorySystem.BLL.Services;
using StoreInventorySystem.DAL.Entities;

namespace StockInventorySytem
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePathCategories = "categories.txt";
            string filePathProducts = "products.txt";
            string filePathTransactions = "transactions.txt";

            string direction = @"C:\PB501Files\StoreInventorySystem";

            string fullPathCategories = Path.Combine(direction, filePathCategories);
            string fullPathProducts = Path.Combine(direction, filePathProducts);
            string fullPathTransactions = Path.Combine(direction, filePathTransactions);

            if (!Directory.Exists(direction))
                Directory.CreateDirectory(direction);

            Program program = new Program();
            program.Menu(fullPathCategories, fullPathProducts, fullPathTransactions);
        }

        public void Menu(string catPath, string prodPath, string transPath)
        {
            while (true)
            {
                Console.WriteLine("\n=== STORE INVENTORY SYSTEM ===");
                Console.WriteLine("1. Add Category");
                Console.WriteLine("2. List Categories");
                Console.WriteLine("3. Add Product");
                Console.WriteLine("4. List Products");
                Console.WriteLine("5. Increase Stock");
                Console.WriteLine("6. Decrease Stock");
                Console.WriteLine("7. List Stock Transactions");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCategory(catPath);
                        break;
                    case "2":
                        ListCategories(catPath);
                        break;
                    case "3":
                        AddProduct(prodPath, catPath);
                        break;
                    case "4":
                        ListProducts(prodPath);
                        break;
                    case "5":
                        IncreaseStock(prodPath, transPath);
                        break;
                    case "6":
                        DecreaseStock(prodPath, transPath);
                        break;
                    case "7":
                        ListTransactions(transPath);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        public void AddCategory(string catPath)
        {
            Console.Write("Enter category name: ");
            string name = Console.ReadLine();

            try
            {
                var service = new CategoryService(catPath);
                service.AddCategory(name);
                Console.WriteLine("Category added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListCategories(string catPath)
        {
            var service = new CategoryService(catPath);
            var categories = service.GetAllCategories();

            if (!categories.Any())
            {
                Console.WriteLine("No categories found.");
                return;
            }

            foreach (var cat in categories)
                Console.WriteLine(cat);
        }

        public void AddProduct(string prodPath, string catPath)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter price: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.Write("Enter valid price: ");
            }

            Console.Write("Enter category id: ");
            int catId;
            while (!int.TryParse(Console.ReadLine(), out catId) || catId <= 0)
            {
                Console.Write("Enter valid category id: ");
            }

            var product = new Product
            {
                Name = name,
                Price = price,
                CategoryId = catId,
                StockCount = 0
            };

            try
            {
                var service = new ProductService(prodPath, catPath);
                service.AddProduct(product);
                Console.WriteLine("Product added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListProducts(string prodPath)
        {
            var service = new ProductService(prodPath, "");
            var products = service.GetAllProducts();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (var prod in products)
                Console.WriteLine(prod);
        }

        public void IncreaseStock(string prodPath, string transPath)
        {
            Console.Write("Enter product id: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid id!");
                return;
            }

            Console.Write("Enter quantity: ");
            int qty;
            if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                return;
            }

            try
            {
                var service = new StockService(prodPath, transPath);
                service.IncreaseStock(id, qty);
                Console.WriteLine("Stock increased successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DecreaseStock(string prodPath, string transPath)
        {
            Console.Write("Enter product id: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid id!");
                return;
            }

            Console.Write("Enter quantity: ");
            int qty;
            if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                return;
            }

            try
            {
                var service = new StockService(prodPath, transPath);
                service.DecreaseStock(id, qty);
                Console.WriteLine("Stock decreased successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListTransactions(string transPath)
        {
            var service = new StockService("", transPath);
            var transactions = service.GetAllTransactions();

            if (!transactions.Any())
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            foreach (var t in transactions)
                Console.WriteLine(t);
        }
    }
}