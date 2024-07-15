using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ControlInventoryManagment.DTOs.Categorie;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.Services
{
    public class CategorieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategorieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategorieReadDTO> GetCategorieById(int id)
        {
            return await _unitOfWork.Categories.GetCategorieById(id);
        }

        public async Task<CategorieReadDTO> GetCategorieByName(string name)
        {
            return await _unitOfWork.Categories.GetCategorieByName(name);
        }

        public async Task<CategorieReadDTO> CreateCategorie(CategorieCreateDTO newCategorieDTO)
        {
            var categorie = await _unitOfWork.Categories.CreateCategorie(newCategorieDTO);
            await _unitOfWork.CommitAsync();
            return categorie;
        }

        public async Task UpdateCategorie(CategorieUpdateDTO updatedCategorieDTO)
        {
            await _unitOfWork.Categories.UpdateCategorie(updatedCategorieDTO);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCategorie(CategorieReadDTO categorie)
        {
            await _unitOfWork.Categories.DeleteCategorie(categorie);
            await _unitOfWork.CommitAsync();
        }
    }
}
