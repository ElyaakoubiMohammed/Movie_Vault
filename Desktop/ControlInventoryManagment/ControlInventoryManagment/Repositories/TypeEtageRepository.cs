using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.TypeEtage;
using ControlInventoryManagment.Exceptions;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class TypeEtageRepository : ITypeEtageRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public TypeEtageRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TypeEtageReadDTO> GetTypeEtageById(int id)
        {
            var typeEtage = await _db.TypeEtages.FindAsync(id);
            if (typeEtage == null)
            {
                throw new NotFoundException($"TypeEtage with id {id} not found.");
            }

            return _mapper.Map<TypeEtageReadDTO>(typeEtage);
        }

        public async Task<TypeEtageReadDTO> CreateTypeEtage(TypeEtageCreateDTO newTypeEtage)
        {
            var typeEtageEntity = _mapper.Map<TypeEtage>(newTypeEtage);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.TypeEtages.AddAsync(typeEtageEntity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<TypeEtageReadDTO>(typeEtageEntity);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException("Failed to save TypeEtage.");
                }
            }
        }

        public async Task UpdateTypeEtage(TypeEtageUpdateDTO updatedTypeEtage)
        {
            var existingTypeEtage = await _db.TypeEtages.FindAsync(updatedTypeEtage.Id);
            if (existingTypeEtage == null)
            {
                throw new NotFoundException($"TypeEtage with id {updatedTypeEtage.Id} not found.");
            }

            _mapper.Map(updatedTypeEtage, existingTypeEtage);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.TypeEtages.Update(existingTypeEtage);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotUpdatedException("Failed to update TypeEtage.");
                }
            }
        }

        public async Task DeleteTypeEtage(int id)
        {
            var typeEtage = await _db.TypeEtages.FindAsync(id);
            if (typeEtage == null)
            {
                throw new NotFoundException($"TypeEtage with id {id} not found.");
            }

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.TypeEtages.Remove(typeEtage);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotDeletedException("Failed to delete TypeEtage.");
                }
            }
        }

        public async Task<TypeEtageReadDTO> GetTypeEtageByName(string name)
        {
            var typeEtage = await _db.TypeEtages.FirstOrDefaultAsync(te => te.Name == name);
            if (typeEtage == null)
            {
                throw new NotFoundException($"TypeEtage with name '{name}' not found.");
            }

            return _mapper.Map<TypeEtageReadDTO>(typeEtage);
        }
    }
}
