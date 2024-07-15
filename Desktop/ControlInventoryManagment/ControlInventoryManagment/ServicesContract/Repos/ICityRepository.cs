using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface ICityRepository
    {
        Task<CityReadDTO> GetCityById(int id);
        Task<CityReadDTO> GetCityByName(string name);
        Task<City> CreateCity(CityCreateDTO newCity);
        Task UpdateCity(CityUpdateDTO updatedCity);
        Task DeleteCity(CityReadDTO city);
    }
}
