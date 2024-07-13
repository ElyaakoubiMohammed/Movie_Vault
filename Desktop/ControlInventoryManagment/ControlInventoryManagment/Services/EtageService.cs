using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class EtageService : IEtageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EtageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Etage>> GetAllEtages()
        {
            return await _unitOfWork.Etages.GetAllEtage();
        }

        public async Task<Etage> GetEtageById(int id)
        {
            return await _unitOfWork.Etages.GetEtageById(id);
        }

        public async Task<Etage> CreateEtage(Etage newEtage)
        {
            await _unitOfWork.Etages.CreateEtage(newEtage);
            await _unitOfWork.CommitAsync(); // Save changes after creating etage
            return newEtage; // Return the created etage
        }

        public async Task UpdateEtage(Etage updatedEtage)
        {
            await _unitOfWork.Etages.UpdateEtage(updatedEtage);
            await _unitOfWork.CommitAsync(); // Save changes after updating etage
        }

        public async Task DeleteEtage(Etage etage)
        {
            await _unitOfWork.Etages.DeleteEtage(etage);
            await _unitOfWork.CommitAsync(); // Save changes after deleting etage
        }
    }
}
