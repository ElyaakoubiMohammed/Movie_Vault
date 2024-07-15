using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.Exceptions;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CityRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CityReadDTO> GetCityById(int id)
        {
            City? city = await _db.Cities.FindAsync(id);

            if (city == null)
            {
                throw new NotFoundException($"City with id {id} not found.");
            }

            return _mapper.Map<CityReadDTO>(city);
        }

        public async Task<CityReadDTO> GetCityByName(string name)
        {
            City? city = await _db.Cities.FirstOrDefaultAsync(c => c.Name == name);

            if (city == null)
            {
                throw new NotFoundException($"City with name {name} not found.");
            }

            return _mapper.Map<CityReadDTO>(city);
        }

        public async Task<City> CreateCity(CityCreateDTO newCityDTO)
        {
            var newCity = _mapper.Map<City>(newCityDTO);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Cities.AddAsync(newCity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return newCity;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateCity(CityUpdateDTO updatedCityDTO)
        {
            var existingCity = await _db.Cities.FindAsync(updatedCityDTO.Id);

            if (existingCity == null)
            {
                throw new NotFoundException($"City with id {updatedCityDTO.Id} not found.");
            }

            _mapper.Map(updatedCityDTO, existingCity);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Cities.Update(existingCity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotUpdatedException();
                }
            }
        }

        public async Task DeleteCity(City city)
        {
            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Cities.Remove(city);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotDeletedException();
                }
            }
        }
    }
}
