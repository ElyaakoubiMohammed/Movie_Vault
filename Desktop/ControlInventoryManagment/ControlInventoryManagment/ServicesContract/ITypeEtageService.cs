using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract
{
    public interface ITypeEtageService
    {
        Task<IEnumerable<TypeEtage>> GetAllTypeEtages();
        Task<TypeEtage> GetTypeEtageById(int id);
        Task<TypeEtage> CreateTypeEtage(TypeEtage newTypeEtage);
        Task UpdateTypeEtage(TypeEtage updatedTypeEtage);
        Task DeleteTypeEtage(TypeEtage typeEtage);
    }
}
