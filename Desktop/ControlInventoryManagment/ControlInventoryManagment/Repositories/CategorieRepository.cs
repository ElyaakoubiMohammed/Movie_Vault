using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly AppDbContext _db;

        public CategorieRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Categorie>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Categorie> GetCategorieById(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Categorie> CreateCategorie(Categorie newCategorie)
        {
            await _db.Categories.AddAsync(newCategorie);
            await _db.SaveChangesAsync();
            return newCategorie;
        }

        public async Task UpdateCategorie(Categorie updatedCategorie)
        {
            _db.Categories.Update(updatedCategorie);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCategorie(Categorie categorie)
        {
            _db.Categories.Remove(categorie);
            await _db.SaveChangesAsync();
        }
    }
}