using StoreInventorySystem.DAL.Entities;
using StoreInventorySystem.DAL.Enums;
using StoreInventorySystem.DAL.Interfaces;

namespace StoreInventorySystem.BLL.Services
{
    public class StockService
    {
        private readonly IProductRepository _productRepo;
        private readonly IStockTransactionRepository _transRepo;

        public StockService(IProductRepository productRepo, IStockTransactionRepository transRepo)
        {
            _productRepo = productRepo;
            _transRepo = transRepo;
        }

        public void IncreaseStock(int productId, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero!");

            var products = _productRepo.GetAll();
            var product = products.FirstOrDefault(p => p.Id == productId && p.IsActive);

            if (product == null)
                throw new Exception("Product not found or inactive!");

            product.StockCount += quantity;

            _productRepo.Update(products);

            AddTransaction(productId, quantity, TransactionType.In);
        }

        public void DecreaseStock(int productId, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero!");

            var products = _productRepo.GetAll();
            var product = products.FirstOrDefault(p => p.Id == productId && p.IsActive);

            if (product == null)
                throw new Exception("Product not found or inactive!");

            if (product.StockCount < quantity)
                throw new Exception("Exceeding the available stock is not allowed!");

            product.StockCount -= quantity;

            _productRepo.Update(products);

            AddTransaction(productId, quantity, TransactionType.Out);
        }

        public List<StockTransaction> GetAllTransactions()
        {
            return _transRepo.GetAll();
        }

        private void AddTransaction(int productId, int quantity, TransactionType type)
        {
            var transactions = _transRepo.GetAll();
            int newId = transactions.Any() ? transactions.Max(t => t.Id) + 1 : 1;

            _transRepo.Add(new StockTransaction
            {
                Id = newId,
                ProductId = productId,
                Quantity = quantity,
                TransactionType = type,
                Date = DateTime.Now
            });
        }
    }
}
