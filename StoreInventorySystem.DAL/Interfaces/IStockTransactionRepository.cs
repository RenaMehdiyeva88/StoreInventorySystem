using StoreInventorySystem.DAL.Entities;

namespace StoreInventorySystem.DAL.Interfaces
{
    public interface IStockTransactionRepository
    {
        void Add(StockTransaction transaction);
        List<StockTransaction> GetAll();
    }
}

