using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class TypeEtageService : ITypeEtageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeEtageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TypeEtage>> GetAllTypeEtages()
        {
            return await _unitOfWork.TypeEtages.GetAllTypeEtage();
        }

        public async Task<TypeEtage> GetTypeEtageById(int id)
        {
            return await _unitOfWork.TypeEtages.GetTypeEtageById(id);
        }

        public async Task<TypeEtage> CreateTypeEtage(TypeEtage newTypeEtage)
        {
            await _unitOfWork.TypeEtages.CreateTypeEtage(newTypeEtage);
            await _unitOfWork.CommitAsync(); // Save changes after creating type etage
            return newTypeEtage; // Return the created type etage
        }

        public async Task UpdateTypeEtage(TypeEtage updatedTypeEtage)
        {
            await _unitOfWork.TypeEtages.UpdateTypeEtage(updatedTypeEtage);
            await _unitOfWork.CommitAsync(); // Save changes after updating type etage
        }

        public async Task DeleteTypeEtage(TypeEtage typeEtage)
        {
            await _unitOfWork.TypeEtages.DeleteTypeEtage(typeEtage);
            await _unitOfWork.CommitAsync(); // Save changes after deleting type etage
        }
    }
}
