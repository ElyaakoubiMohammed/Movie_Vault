using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IOperationRepository
    {
        Task<IEnumerable<Operation>> GetAllOperations();
        Task<Operation> GetOperationById(int id);
        Task<Operation> CreateOperation(Operation newOperation);
        Task UpdateOperation(Operation updatedOperation);
        Task DeleteOperation(Operation operation);
    }
}
