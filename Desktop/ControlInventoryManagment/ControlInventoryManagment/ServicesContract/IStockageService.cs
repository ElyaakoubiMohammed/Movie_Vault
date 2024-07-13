using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract
{
    public interface IStockageService
    {
        Task<IEnumerable<Stockage>> GetAllStockages();
        Task<Stockage> GetStockageById(int id);
        Task<Stockage> CreateStockage(Stockage newStockage);
        Task UpdateStockage(Stockage updatedStockage);
        Task DeleteStockage(Stockage stockage);
    }
}
