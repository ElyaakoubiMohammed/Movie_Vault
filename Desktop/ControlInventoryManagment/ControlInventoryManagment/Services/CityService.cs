using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _unitOfWork.Cities.GetAllCities();
        }

        public async Task<City> GetCityById(int id)
        {
            return await _unitOfWork.Cities.GetCityById(id);
        }

        public async Task<City> CreateCity(City newCity)
        {
            await _unitOfWork.Cities.CreateCity(newCity);
            await _unitOfWork.CommitAsync(); // Save changes after creating city
            return newCity; // Return the created city
        }

        public async Task UpdateCity(City updatedCity)
        {
            await _unitOfWork.Cities.UpdateCity(updatedCity);
            await _unitOfWork.CommitAsync(); // Save changes after updating city
        }

        public async Task DeleteCity(City city)
        {
            await _unitOfWork.Cities.DeleteCity(city);
            await _unitOfWork.CommitAsync(); // Save changes after deleting city
        }
    }
}
