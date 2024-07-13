using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class StockageService : IStockageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Stockage>> GetAllStockages()
        {
            return await _unitOfWork.Stockages.GetAllStockage();
        }

        public async Task<Stockage> GetStockageById(int id)
        {
            return await _unitOfWork.Stockages.GetStockageById(id);
        }

        public async Task<Stockage> CreateStockage(Stockage newStockage)
        {
            await _unitOfWork.Stockages.CreateStockage(newStockage);
            await _unitOfWork.CommitAsync(); // Save changes after creating stockage
            return newStockage; // Return the created stockage
        }

        public async Task UpdateStockage(Stockage updatedStockage)
        {
            await _unitOfWork.Stockages.UpdateStockage(updatedStockage);
            await _unitOfWork.CommitAsync(); // Save changes after updating stockage
        }

        public async Task DeleteStockage(Stockage stockage)
        {
            await _unitOfWork.Stockages.DeleteStockage(stockage);
            await _unitOfWork.CommitAsync(); // Save changes after deleting stockage
        }
    }
}
