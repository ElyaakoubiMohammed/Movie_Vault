using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ControlInventoryManagment.ServicesContract.Repos;

namespace ControlInventoryManagment.Repositories
{
    public class EntrepriseRepository : IEntrepriseRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public EntrepriseRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<EntrepriseReadDTO> GetEntrepriseById(int id)
        {
            Entreprise? entreprise = await _db.Entreprises
                .Include(e => e.Locals)
                .ThenInclude(l => l.Etages)
                .ThenInclude(e => e.Stockages)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entreprise == null)
            {
                throw new NotFoundException($"Enterprise with id {id} not found.");
            }

            return _mapper.Map<EntrepriseReadDTO>(entreprise);
        }

        public async Task<EntrepriseReadDTO> GetEntrepriseByName(string name)
        {
            Entreprise? entreprise = await _db.Entreprises
                .Include(e => e.Locals)
                .ThenInclude(l => l.Etages)
                .ThenInclude(e => e.Stockages)
                .FirstOrDefaultAsync(e => e.Name == name);

            if (entreprise == null)
            {
                throw new NotFoundException($"Enterprise with name {name} not found.");
            }

            return _mapper.Map<EntrepriseReadDTO>(entreprise);
        }

        public async Task<EntrepriseReadDTO> CreateEntreprise(EntrepriseCreateDTO newEntrepriseDTO)
        {
            var newEntreprise = _mapper.Map<Entreprise>(newEntrepriseDTO);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Entreprises.AddAsync(newEntreprise);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<EntrepriseReadDTO>(newEntreprise);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateEntreprise(EntrepriseUpdateDTO updatedEntrepriseDTO)
        {
            var existingEntreprise = await _db.Entreprises.FindAsync(updatedEntrepriseDTO.Id);

            if (existingEntreprise == null)
            {
                throw new NotFoundException($"Enterprise with id {updatedEntrepriseDTO.Id} not found.");
            }

            _mapper.Map(updatedEntrepriseDTO, existingEntreprise);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Entreprises.Update(existingEntreprise);
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

        public async Task DeleteEntreprise(int id)
        {
            var entreprise = await _db.Entreprises.FindAsync(id);

            if (entreprise == null)
            {
                throw new NotFoundException($"Enterprise with id {id} not found.");
            }

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Entreprises.Remove(entreprise);
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
