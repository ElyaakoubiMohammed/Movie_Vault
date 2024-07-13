using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IEtageRepository
    {
        Task<IEnumerable<Etage>> GetAllEtage();
        Task<Etage> GetEtageById(int id);
        Task<Etage> CreateEtage(Etage newEtage);
        Task UpdateEtage(Etage updatedEtage);
        Task DeleteEtage(Etage Etage);
    }
}