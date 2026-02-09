using StoreInventorySystem.DAL.Entities;
using StoreInventorySystem.DAL.Enums;
using StoreInventorySystem.DAL.Interfaces;

namespace StoreInventorySystem.DAL.Repositories
{
    public class StockTransactionRepository : IStockTransactionRepository
    {
        private readonly string _filePath;

        public StockTransactionRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(StockTransaction transaction)
        {
            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine($"{transaction.Id}|{transaction.ProductId}|{transaction.Quantity}|{transaction.TransactionType}|{transaction.Date:yyyy-MM-dd HH:mm:ss}");
        }

        public List<StockTransaction> GetAll()
        {
            var list = new List<StockTransaction>();
            if (!File.Exists(_filePath)) return list;

            using var sr = new StreamReader(_filePath);
            while (!sr.EndOfStream)
            {
                var parts = sr.ReadLine().Split('|');
                list.Add(new StockTransaction
                {
                    Id = int.Parse(parts[0]),
                    ProductId = int.Parse(parts[1]),
                    Quantity = int.Parse(parts[2]),
                    TransactionType = Enum.Parse<TransactionType>(parts[3]),
                    Date = DateTime.Parse(parts[4])
                });
            }
            return list;
        }
    }
}

