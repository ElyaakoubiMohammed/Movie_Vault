using AutoMapper;
using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class CityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CityReadDTO> GetCityById(int id)
        {
            return await _unitOfWork.Cities.GetCityById(id);
        }

        public async Task<CityReadDTO> GetCityByName(string name)
        {
            return await _unitOfWork.Cities.GetCityByName(name);
        }

        public async Task<City> CreateCity(CityCreateDTO newCityDTO)
        {
            var city = await _unitOfWork.Cities.CreateCity(newCityDTO);
            await _unitOfWork.CommitAsync();
            return city;
        }

        public async Task UpdateCity(CityUpdateDTO updatedCityDTO)
        {
            await _unitOfWork.Cities.UpdateCity(updatedCityDTO);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCity(CityReadDTO city)
        {
            await _unitOfWork.Cities.DeleteCity(city);
            await _unitOfWork.CommitAsync();
        }
    }
}
