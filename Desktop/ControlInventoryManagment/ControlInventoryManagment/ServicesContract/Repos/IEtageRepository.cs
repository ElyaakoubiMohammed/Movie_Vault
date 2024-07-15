using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IEtageRepository
    {
        Task<EtageReadDTO> GetEtageById(int id);
        Task<EtageReadDTO> GetEtageByType(string typeEtage);
        Task<Etage> CreateEtage(EtageCreateDTO newEtage);
        Task UpdateEtage(EtageUpdateDTO updatedEtage);
        Task DeleteEtage(Etage etage);
    }
}
