using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs;
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
    public class OperationRepository : IOperationRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public OperationRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OperationReadDTO> GetOperationById(int id)
        {
            var operation = await _db.Operations.FindAsync(id);
            if (operation == null)
            {
                throw new NotFoundException($"Operation with id {id} not found.");
            }

            return _mapper.Map<OperationReadDTO>(operation);
        }

        public async Task<OperationReadDTO> CreateOperation(OperationCreateDTO newOperation)
        {
            var operation = _mapper.Map<Operation>(newOperation);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Operations.AddAsync(operation);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<OperationReadDTO>(operation);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateOperation(OperationUpdateDTO updatedOperation)
        {
            var operation = await _db.Operations.FindAsync(updatedOperation.Id);
            if (operation == null)
            {
                throw new NotFoundException($"Operation with id {updatedOperation.Id} not found.");
            }

            _mapper.Map(updatedOperation, operation);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Operations.Update(operation);
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

        public async Task DeleteOperation(int id)
        {
            var operation = await _db.Operations.FindAsync(id);
            if (operation == null)
            {
                throw new NotFoundException($"Operation with id {id} not found.");
            }

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Operations.Remove(operation);
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

        public async Task<IEnumerable<OperationReadDTO>> GetOperationsByDateRange(DateTime startDate, DateTime endDate)
        {
            var operations = await _db.Operations
                .Where(o => o.DateDebut >= startDate && o.DateFin <= endDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OperationReadDTO>>(operations);
        }
    }
}
