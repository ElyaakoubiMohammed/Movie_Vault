using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly AppDbContext _dbContext;

        public OperationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Operation>> GetAllOperations()
        {
            return await _dbContext.Operations.ToListAsync();
        }

        public async Task<Operation> GetOperationById(int id)
        {
            return await _dbContext.Operations.FindAsync(id);
        }

        public async Task<Operation> CreateOperation(Operation newOperation)
        {
            await _dbContext.Operations.AddAsync(newOperation);
            await _dbContext.SaveChangesAsync();
            return newOperation;
        }

        public async Task UpdateOperation(Operation updatedOperation)
        {
            _dbContext.Operations.Update(updatedOperation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOperation(Operation operation)
        {
            _dbContext.Operations.Remove(operation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
