using ControlInventoryManagment.DTOs.Categorie;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ICategorieRepository
    {
        Task<CategorieReadDTO> GetCategorieById(int id);
        Task<CategorieReadDTO> GetCategorieByName(string name);
        Task<CategorieReadDTO> CreateCategorie(CategorieCreateDTO newCategorie);
        Task UpdateCategorie(CategorieUpdateDTO updatedCategorie);
        Task DeleteCategorie(CategorieReadDTO categorie);
    }
}
