using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.Local;
using ControlInventoryManagment.Exceptions;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public LocalRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LocalReadDTO> GetLocalById(int id)
        {
            Local? local = await _db.Locals.FindAsync(id);

            if (local == null)
            {
                throw new NotFoundException($"Local with id {id} not found.");
            }

            return _mapper.Map<LocalReadDTO>(local);
        }

        public async Task<LocalReadDTO> CreateLocal(LocalCreateDTO newLocal)
        {
            Local localEntity = _mapper.Map<Local>(newLocal);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Locals.AddAsync(localEntity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<LocalReadDTO>(localEntity);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateLocal(LocalUpdateDTO updatedLocal)
        {
            Local? localEntity = await _db.Locals.FindAsync(updatedLocal.Id);

            if (localEntity == null)
            {
                throw new NotFoundException($"Local with id {updatedLocal.Id} not found.");
            }

            _mapper.Map(updatedLocal, localEntity);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Locals.Update(localEntity);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotUpdatedException($"Failed to update Local with id {updatedLocal.Id}");
                }
            }
        }

        public async Task DeleteLocal(Local local)
        {
            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Locals.Remove(local);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotDeletedException($"Failed to delete Local with id {local.Id}");
                }
            }
        }
    }
}
