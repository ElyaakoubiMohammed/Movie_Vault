using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class CategorieService : ICategorieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategorieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Categorie>> GetAllCategories()
        {
            return await _unitOfWork.Categories.GetAllCategories();
        }

        public async Task<Categorie> GetCategorieById(int id)
        {
            return await _unitOfWork.Categories.GetCategorieById(id);
        }

        public async Task<Categorie> CreateCategorie(Categorie newCategorie)
        {
            await _unitOfWork.Categories.CreateCategorie(newCategorie);
            await _unitOfWork.CommitAsync(); // Save changes after creating category
            return newCategorie; // Return the created category
        }

        public async Task UpdateCategorie(Categorie updatedCategorie)
        {
            await _unitOfWork.Categories.UpdateCategorie(updatedCategorie);
            await _unitOfWork.CommitAsync(); // Save changes after updating category
        }

        public async Task DeleteCategorie(Categorie categorie)
        {
            await _unitOfWork.Categories.DeleteCategorie(categorie);
            await _unitOfWork.CommitAsync();
        }
    }
}
