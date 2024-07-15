using ControlInventoryManagment.DTOs.Stockage;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IStockageRepository
    {
        Task<StockageReadDTO> GetStockageById(int id);
        Task<StockageReadDTO> CreateStockage(StockageCreateDTO newStockage);
        Task UpdateStockage(int id, StockageUpdateDTO updatedStockage);
        Task DeleteStockage(int id);
    }
}
