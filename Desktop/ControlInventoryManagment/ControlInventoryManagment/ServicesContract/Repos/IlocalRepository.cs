using ControlInventoryManagment.DTOs.Local;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ILocalRepository
    {
        Task<LocalReadDTO> GetLocalById(int id);
        Task<LocalReadDTO> CreateLocal(LocalCreateDTO newLocal);
        Task UpdateLocal(LocalUpdateDTO updatedLocal);
        Task DeleteLocal(Local local);
    }
}
