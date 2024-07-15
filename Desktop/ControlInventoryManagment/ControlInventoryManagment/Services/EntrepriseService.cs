using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class EntrepriseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntrepriseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EntrepriseReadDTO> GetEntrepriseById(int id)
        {
            return await _unitOfWork.Entreprises.GetEntrepriseById(id);
        }

        public async Task<EntrepriseReadDTO> GetEntrepriseByName(string name)
        {
            return await _unitOfWork.Entreprises.GetEntrepriseByName(name);
        }

        public async Task<EntrepriseReadDTO> CreateEntreprise(EntrepriseCreateDTO newEntrepriseDTO)
        {
            var entreprise = await _unitOfWork.Entreprises.CreateEntreprise(newEntrepriseDTO);
            await _unitOfWork.CommitAsync();
            return entreprise;
        }

        public async Task UpdateEntreprise(EntrepriseUpdateDTO updatedEntrepriseDTO)
        {
            await _unitOfWork.Entreprises.UpdateEntreprise(updatedEntrepriseDTO);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEntreprise(int id)
        {
            await _unitOfWork.Entreprises.DeleteEntreprise(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
