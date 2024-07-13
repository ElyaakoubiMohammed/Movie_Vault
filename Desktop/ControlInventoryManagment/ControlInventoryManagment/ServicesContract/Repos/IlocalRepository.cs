
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ILocalRepository
    {
        Task<IEnumerable<Local>> GetAlllocal();
        Task<Local> GetlocalById(int id);
        Task<Local> Createlocal(Local newlocal);
        Task UpdateLocal(Local updatedlocal);
        Task Deletelocal(Local local);
    }
}