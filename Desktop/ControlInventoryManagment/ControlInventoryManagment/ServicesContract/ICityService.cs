using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCities();
        Task<City> GetCityById(int id);
        Task<City> CreateCity(City newCity);
        Task UpdateCity(City updatedCity);
        Task DeleteCity(City city);
    }
}
