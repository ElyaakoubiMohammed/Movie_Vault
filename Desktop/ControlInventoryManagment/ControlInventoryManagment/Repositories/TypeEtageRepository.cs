using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class TypeEtageRepository : ITypeEtagenRepository
    {
        private readonly AppDbContext _dbContext;

        public TypeEtageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TypeEtage>> GetAllTypeEtage()
        {
            return await _dbContext.TypeEtages.ToListAsync();
        }

        public async Task<TypeEtage> GetTypeEtageById(int id)
        {
            return await _dbContext.TypeEtages.FindAsync(id);
        }

        public async Task<TypeEtage> CreateTypeEtage(TypeEtage newTypeEtage)
        {
            await _dbContext.TypeEtages.AddAsync(newTypeEtage);
            await _dbContext.SaveChangesAsync();
            return newTypeEtage;
        }

        public async Task UpdateTypeEtage(TypeEtage updatedTypeEtage)
        {
            _dbContext.TypeEtages.Update(updatedTypeEtage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTypeEtage(TypeEtage typeEtage)
        {
            _dbContext.TypeEtages.Remove(typeEtage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
