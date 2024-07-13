using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ITypeEtagenRepository
    {
        Task<IEnumerable<TypeEtage>> GetAllTypeEtage();
        Task<TypeEtage> GetTypeEtageById(int id);
        Task<TypeEtage> CreateTypeEtage(TypeEtage newTypeEtage);
        Task UpdateTypeEtage(TypeEtage updatedTypeEtage);
        Task DeleteTypeEtage(TypeEtage TypeEtage);
    }
}
