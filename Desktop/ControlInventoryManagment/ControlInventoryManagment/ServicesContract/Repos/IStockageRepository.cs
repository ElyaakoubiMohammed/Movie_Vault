using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IStockageRepository
    {
        Task<IEnumerable<Stockage>> GetAllStockage();
        Task<Stockage> GetStockageById(int id);
        Task<Stockage> CreateStockage(Stockage newSockage);
        Task UpdateStockage(Stockage updatedStockage);
        Task DeleteStockage(Stockage stockage);
    }
}
