using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class StockageRepository : IStockageRepository
    {
        private readonly AppDbContext _dbContext;

        public StockageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Stockage>> GetAllStockage()
        {
            return await _dbContext.Stockages.ToListAsync();
        }

        public async Task<Stockage> GetStockageById(int id)
        {
            return await _dbContext.Stockages.FindAsync(id);
        }

        public async Task<Stockage> CreateStockage(Stockage newStockage)
        {
            await _dbContext.Stockages.AddAsync(newStockage);
            await _dbContext.SaveChangesAsync();
            return newStockage;
        }

        public async Task UpdateStockage(Stockage updatedStockage)
        {
            _dbContext.Stockages.Update(updatedStockage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStockage(Stockage stockage)
        {
            _dbContext.Stockages.Remove(stockage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
