using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class LocalService : ILocalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Local>> GetAllLocals()
        {
            return await _unitOfWork.Locals.GetAlllocal();
        }

        public async Task<Local> GetLocalById(int id)
        {
            return await _unitOfWork.Locals.GetlocalById(id);
        }

        public async Task<Local> CreateLocal(Local newLocal)
        {
            await _unitOfWork.Locals.Createlocal(newLocal);
            await _unitOfWork.CommitAsync(); // Save changes after creating local
            return newLocal; // Return the created local
        }

        public async Task UpdateLocal(Local updatedLocal)
        {
            await _unitOfWork.Locals.UpdateLocal(updatedLocal);
            await _unitOfWork.CommitAsync(); // Save changes after updating local
        }

        public async Task DeleteLocal(Local local)
        {
            await _unitOfWork.Locals.Deletelocal(local);
            await _unitOfWork.CommitAsync(); // Save changes after deleting local
        }
    }
}
