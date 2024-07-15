using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.Categorie;
using ControlInventoryManagment.Exceptions;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CategorieRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategorieReadDTO> GetCategorieById(int id)
        {
            Categorie? categorie = await _db.Categories.FindAsync(id);

            if (categorie == null)
            {
                throw new NotFoundException($"Category with id {id} not found.");
            }

            return _mapper.Map<CategorieReadDTO>(categorie);
        }

        public async Task<CategorieReadDTO> GetCategorieByName(string name)
        {
            Categorie? categorie = await _db.Categories.FirstOrDefaultAsync(c => c.Name == name);

            if (categorie == null)
            {
                throw new NotFoundException($"Category with name {name} not found.");
            }

            return _mapper.Map<CategorieReadDTO>(categorie);
        }

        public async Task<CategorieReadDTO> CreateCategorie(CategorieCreateDTO newCategorieDTO)
        {
            var newCategorie = _mapper.Map<Categorie>(newCategorieDTO);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Categories.AddAsync(newCategorie);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<CategorieReadDTO>(newCategorie);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateCategorie(CategorieUpdateDTO updatedCategorieDTO)
        {
            var existingCategorie = await _db.Categories.FindAsync(updatedCategorieDTO.Id);

            if (existingCategorie == null)
            {
                throw new NotFoundException($"Category with id {updatedCategorieDTO.Id} not found.");
            }

            _mapper.Map(updatedCategorieDTO, existingCategorie);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Categories.Update(existingCategorie);
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

        public async Task DeleteCategorie(CategorieReadDTO categorie)
        {
            var existingCategorie = await _db.Categories.FindAsync(categorie.Id);

            if (existingCategorie == null)
            {
                throw new NotFoundException($"Category with id {categorie.Id} not found.");
            }

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Categories.Remove(existingCategorie);
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
