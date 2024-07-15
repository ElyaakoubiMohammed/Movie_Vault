using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.Stockage;
using ControlInventoryManagment.Exceptions;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class StockageRepository : IStockageRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public StockageRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<StockageReadDTO> GetStockageById(int id)
        {
            var stockage = await _db.Stockages.FindAsync(id);
            if (stockage == null)
            {
                throw new NotFoundException($"Stockage with id {id} not found.");
            }

            return _mapper.Map<StockageReadDTO>(stockage);
        }

        public async Task<StockageReadDTO> CreateStockage(StockageCreateDTO newStockage)
        {
            var stockageEntity = _mapper.Map<Stockage>(newStockage);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Stockages.AddAsync(stockageEntity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<StockageReadDTO>(stockageEntity);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException("Failed to save Stockage.");
                }
            }
        }

        public async Task UpdateStockage(int id, StockageUpdateDTO updatedStockage)
        {
            var stockageEntity = await _db.Stockages.FindAsync(id);
            if (stockageEntity == null)
            {
                throw new NotFoundException($"Stockage with id {id} not found.");
            }

            _mapper.Map(updatedStockage, stockageEntity);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Stockages.Update(stockageEntity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotUpdatedException("Failed to update Stockage.");
                }
            }
        }

        public async Task DeleteStockage(int id)
        {
            var stockageEntity = await _db.Stockages.FindAsync(id);
            if (stockageEntity == null)
            {
                throw new NotFoundException($"Stockage with id {id} not found.");
            }

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Stockages.Remove(stockageEntity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotDeletedException("Failed to delete Stockage.");
                }
            }
        }
    }
}
