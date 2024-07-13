using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract;
using ControlInventoryManagment.ServicesContract.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _unitOfWork.Products.GetAllProduct();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.Products.GetProductById(id);
        }

        public async Task<Product> CreateProduct(Product newProduct)
        {
            await _unitOfWork.Products.CreateProduct(newProduct);
            await _unitOfWork.CommitAsync(); // Save changes after creating product
            return newProduct; // Return the created product
        }

        public async Task UpdateProduct(Product updatedProduct)
        {
            await _unitOfWork.Products.UpdateProduct(updatedProduct);
            await _unitOfWork.CommitAsync(); // Save changes after updating product
        }

        public async Task DeleteProduct(Product product)
        {
            await _unitOfWork.Products.DeleteProduct(product);
            await _unitOfWork.CommitAsync(); // Save changes after deleting product
        }
    }
}
