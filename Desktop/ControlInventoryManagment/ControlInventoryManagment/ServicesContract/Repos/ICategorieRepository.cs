using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ICategorieRepository
    {
        Task<IEnumerable<Categorie>> GetAllCategories();
        Task<Categorie> GetCategorieById(int id);
        Task<Categorie> CreateCategorie(Categorie newCategorie);
        Task UpdateCategorie(Categorie updatedCategorie);
        Task DeleteCategorie(Categorie categorie);
    }
}