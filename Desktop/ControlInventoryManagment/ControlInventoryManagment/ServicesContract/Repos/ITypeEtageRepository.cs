using ControlInventoryManagment.DTOs.TypeEtage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ITypeEtageRepository
    {
        Task<TypeEtageReadDTO> GetTypeEtageById(int id);
        Task<TypeEtageReadDTO> CreateTypeEtage(TypeEtageCreateDTO newTypeEtage);
        Task UpdateTypeEtage(TypeEtageUpdateDTO updatedTypeEtage);
        Task DeleteTypeEtage(int id);
        Task<TypeEtageReadDTO> GetTypeEtageByName(string name);
    }
}
