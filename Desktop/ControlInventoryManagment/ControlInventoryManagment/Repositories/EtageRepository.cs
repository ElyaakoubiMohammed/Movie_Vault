using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;


namespace ControlInventoryManagment.Repositories
{
    public class EtageRepository : IEtageRepository
    {
        private readonly AppDbContext _dbContext;

        public EtageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Etage>> GetAllEtage()
        {
            return await _dbContext.Etages.ToListAsync();
        }

        public async Task<Etage> GetEtageById(int id)
        {
            return await _dbContext.Etages.FindAsync(id);
        }

        public async Task<Etage> CreateEtage(Etage newEtage)
        {
            await _dbContext.Etages.AddAsync(newEtage);
            await _dbContext.SaveChangesAsync();
            return newEtage;
        }

        public async Task UpdateEtage(Etage updatedEtage)
        {
            _dbContext.Etages.Update(updatedEtage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEtage(Etage etage)
        {
            _dbContext.Etages.Remove(etage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
