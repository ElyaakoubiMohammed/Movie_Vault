using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.Exceptions;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class EtageRepository : IEtageRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public EtageRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<EtageReadDTO> GetEtageById(int id)
        {
            Etage? etage = await _db.Etages.FindAsync(id);
            if (etage == null)
            {
                throw new NotFoundException($"Etage with id {id} not found.");
            }
            return _mapper.Map<EtageReadDTO>(etage);
        }

        public async Task<EtageReadDTO> GetEtageByType(string typeEtage)
        {
            Etage? etage = await _db.Etages.FirstOrDefaultAsync(e => e.TypeEtage == typeEtage);
            if (etage == null)
            {
                throw new NotFoundException($"Etage with TypeEtage {typeEtage} not found.");
            }
            return _mapper.Map<EtageReadDTO>(etage);
        }

        public async Task<Etage> CreateEtage(EtageCreateDTO newEtage)
        {
            var etage = _mapper.Map<Etage>(newEtage);
            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Etages.AddAsync(etage);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return etage;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateEtage(EtageUpdateDTO updatedEtage)
        {
            var etage = _mapper.Map<Etage>(updatedEtage);
            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Etages.Update(etage);
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

        public async Task DeleteEtage(Etage etage)
        {
            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Etages.Remove(etage);
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
