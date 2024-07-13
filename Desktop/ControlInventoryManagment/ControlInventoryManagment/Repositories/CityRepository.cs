using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _dbContext;

        public CityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _dbContext.Cities.ToListAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            return await _dbContext.Cities.FindAsync(id);
        }

        public async Task<City> CreateCity(City newCity)
        {
            await _dbContext.Cities.AddAsync(newCity);
            await _dbContext.SaveChangesAsync();
            return newCity;
        }

        public async Task UpdateCity(City updatedCity)
        {
            _dbContext.Cities.Update(updatedCity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCity(City city)
        {
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
        }
    }
}
