using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract
{
    public interface ILocalService
    {
        Task<IEnumerable<Local>> GetAllLocals();
        Task<Local> GetLocalById(int id);
        Task<Local> CreateLocal(Local newLocal);
        Task UpdateLocal(Local updatedLocal);
        Task DeleteLocal(Local local);
    }
}
