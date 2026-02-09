using StoreInventorySystem.BLL.Services;
using StoreInventorySystem.DAL.Repositories;

namespace StockInventorySytem
{
    public class Program
    {
        private CategoryService _categoryService;
        private ProductService _productService;
        private StockService _stockService;

        static void Main(string[] args)
        {
            string directory = @"C:\PA501Files\StoreInventorySystem";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var catRepo = new CategoryRepository(Path.Combine(directory, "categories.txt"));
            var prodRepo = new ProductRepository(Path.Combine(directory, "products.txt"));
            var transRepo = new StockTransactionRepository(Path.Combine(directory, "transactions.txt"));

            Program program = new Program
            {
                _categoryService = new CategoryService(catRepo),
                _productService = new ProductService(prodRepo, catRepo),
                _stockService = new StockService(prodRepo, transRepo)
            };

            program.Menu();
        }

        public void Menu()
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
                        AddCategory();
                        break;
                    case "2":
                        ListCategories();
                        break;
                    case "3":
                        AddProduct();
                        break;
                    case "4":
                        ListProducts();
                        break;
                    case "5":
                        IncreaseStock();
                        break;
                    case "6":
                        DecreaseStock();
                        break;
                    case "7":
                        ListTransactions();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        private void AddCategory()
        {
            Console.Write("Enter category name: ");
            string name = Console.ReadLine();

            try
            {
                _categoryService.AddCategory(name);
                Console.WriteLine("Category added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ListCategories()
        {
            var categories = _categoryService.GetAllCategories();

            if (!categories.Any())
            {
                Console.WriteLine("No categories found.");
                return;
            }

            foreach (var cat in categories)
            {
                Console.WriteLine(cat);
            }
        }

        private void AddProduct()
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

            try
            {
                _productService.AddProduct(name, price, catId);
                Console.WriteLine("Product added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ListProducts()
        {
            var products = _productService.GetAllProducts();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (var prod in products)
            {
                Console.WriteLine(prod);
            }
        }

        private void IncreaseStock()
        {
            Console.Write("Enter product id: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid id!");
                return;
            }

            Console.Write("Enter quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                return;
            }

            try
            {
                _stockService.IncreaseStock(id, quantity);
                Console.WriteLine("Stock increased successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DecreaseStock()
        {
            Console.Write("Enter product id: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid id!");
                return;
            }

            Console.Write("Enter quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                return;
            }

            try
            {
                _stockService.DecreaseStock(id, quantity);
                Console.WriteLine("Stock decreased successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ListTransactions()
        {
            var transactions = _stockService.GetAllTransactions();

            if (!transactions.Any())
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            foreach (var t in transactions)
            {
                Console.WriteLine(t);
            }
        }
    }
}

