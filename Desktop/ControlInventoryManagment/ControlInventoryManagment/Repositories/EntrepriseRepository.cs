using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class EntrepriseRepository : IEntrepriseRepository
    {
        private readonly AppDbContext _dbContext;

        public EntrepriseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Entreprise>> GetAllEntreprise()
        {
            return await _dbContext.Entreprises.ToListAsync();
        }

        public async Task<Entreprise> GetEntrepriseById(int id)
        {
            return await _dbContext.Entreprises.FindAsync(id);
        }

        public async Task<Entreprise> CreateEntreprise(Entreprise newEntreprise)
        {
            await _dbContext.Entreprises.AddAsync(newEntreprise);
            await _dbContext.SaveChangesAsync();
            return newEntreprise;
        }

        public async Task UpdateEntreprise(Entreprise updatedEntreprise)
        {
            _dbContext.Entreprises.Update(updatedEntreprise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntreprise(Entreprise entreprise)
        {
            _dbContext.Entreprises.Remove(entreprise);
            await _dbContext.SaveChangesAsync();
        }
    }
}
