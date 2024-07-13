using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class OperationService : IOperationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Operation>> GetAllOperations()
        {
            return await _unitOfWork.Operations.GetAllOperations();
        }

        public async Task<Operation> GetOperationById(int id)
        {
            return await _unitOfWork.Operations.GetOperationById(id);
        }

        public async Task<Operation> CreateOperation(Operation newOperation)
        {
            await _unitOfWork.Operations.CreateOperation(newOperation);
            await _unitOfWork.CommitAsync(); // Save changes after creating operation
            return newOperation; // Return the created operation
        }

        public async Task UpdateOperation(Operation updatedOperation)
        {
            await _unitOfWork.Operations.UpdateOperation(updatedOperation);
            await _unitOfWork.CommitAsync(); // Save changes after updating operation
        }

        public async Task DeleteOperation(Operation operation)
        {
            await _unitOfWork.Operations.DeleteOperation(operation);
            await _unitOfWork.CommitAsync(); // Save changes after deleting operation
        }
    }
}
