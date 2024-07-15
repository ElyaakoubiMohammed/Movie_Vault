using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IOperationRepository
    {
        Task<OperationReadDTO> GetOperationById(int id);
        Task<OperationReadDTO> CreateOperation(OperationCreateDTO newOperation);
        Task UpdateOperation(OperationUpdateDTO updatedOperation);
        Task DeleteOperation(int id);
        Task<IEnumerable<OperationReadDTO>> GetOperationsByDateRange(DateTime startDate, DateTime endDate);
    }
}
