using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class EntrepriseService : IEntrepriseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntrepriseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Entreprise>> GetAllEntreprises()
        {
            return await _unitOfWork.Entreprises.GetAllEntreprise();
        }

        public async Task<Entreprise> GetEntrepriseById(int id)
        {
            return await _unitOfWork.Entreprises.GetEntrepriseById(id);
        }

        public async Task<Entreprise> CreateEntreprise(Entreprise newEntreprise)
        {
            await _unitOfWork.Entreprises.CreateEntreprise(newEntreprise);
            await _unitOfWork.CommitAsync(); // Save changes after creating entreprise
            return newEntreprise; // Return the created entreprise
        }

        public async Task UpdateEntreprise(Entreprise updatedEntreprise)
        {
            await _unitOfWork.Entreprises.UpdateEntreprise(updatedEntreprise);
            await _unitOfWork.CommitAsync(); // Save changes after updating entreprise
        }

        public async Task DeleteEntreprise(Entreprise entreprise)
        {
            await _unitOfWork.Entreprises.DeleteEntreprise(entreprise);
            await _unitOfWork.CommitAsync(); // Save changes after deleting entreprise
        }
    }
}
