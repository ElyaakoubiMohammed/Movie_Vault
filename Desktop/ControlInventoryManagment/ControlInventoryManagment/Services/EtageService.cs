using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class EtageService
    {
        private readonly IEtageRepository _etageRepository;

        public EtageService(IEtageRepository etageRepository)
        {
            _etageRepository = etageRepository;
        }

        public Task<EtageReadDTO> GetEtageById(int id)
        {
            return _etageRepository.GetEtageById(id);
        }

        public Task<EtageReadDTO> GetEtageByType(string typeEtage)
        {
            return _etageRepository.GetEtageByType(typeEtage);
        }

        public Task<Etage> CreateEtage(EtageCreateDTO newEtage)
        {
            return _etageRepository.CreateEtage(newEtage);
        }

        public Task UpdateEtage(EtageUpdateDTO updatedEtage)
        {
            return _etageRepository.UpdateEtage(updatedEtage);
        }

        public Task DeleteEtage(Etage etage)
        {
            return _etageRepository.DeleteEtage(etage);
        }
    }
}
