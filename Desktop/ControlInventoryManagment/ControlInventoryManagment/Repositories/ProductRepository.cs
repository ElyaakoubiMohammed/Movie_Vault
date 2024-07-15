using AutoMapper;
using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.DTOs.Product;
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
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductReadDTO> GetProductById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }
            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> CreateProduct(ProductCreateDTO newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.Products.AddAsync(product);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return _mapper.Map<ProductReadDTO>(product);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new NotSavedException();
                }
            }
        }

        public async Task UpdateProduct(ProductUpdateDTO updatedProduct)
        {
            var existingProduct = await _db.Products.FindAsync(updatedProduct.Id);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with id {updatedProduct.Id} not found.");
            }

            _mapper.Map(updatedProduct, existingProduct);

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Products.Update(existingProduct);
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

        public async Task DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }

            await using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Products.Remove(product);
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

        public async Task<ProductReadDTO> GetProductByName(string name)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == name);
            if (product == null)
            {
                throw new NotFoundException($"Product with name '{name}' not found.");
            }
            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> GetProductBySerialNumber(string serialNumber)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.SerialNumber == serialNumber);
            if (product == null)
            {
                throw new NotFoundException($"Product with serial number '{serialNumber}' not found.");
            }
            return _mapper.Map<ProductReadDTO>(product);
        }
    }
}
