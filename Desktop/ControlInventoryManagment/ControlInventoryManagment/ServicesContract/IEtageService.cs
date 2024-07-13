using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract
{
    public interface IEtageService
    {
        Task<IEnumerable<Etage>> GetAllEtages();
        Task<Etage> GetEtageById(int id);
        Task<Etage> CreateEtage(Etage newEtage);
        Task UpdateEtage(Etage updatedEtage);
        Task DeleteEtage(Etage etage);
    }
}
