using ControlInventoryManagment.DTOs.Product;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IProductRepository
    {
        Task<ProductReadDTO> GetProductById(int id);
        Task<ProductReadDTO> CreateProduct(ProductCreateDTO newProduct);
        Task UpdateProduct(ProductUpdateDTO updatedProduct);
        Task DeleteProduct(int id);
        Task<ProductReadDTO> GetProductByName(string name);
        Task<ProductReadDTO> GetProductBySerialNumber(string serialNumber);
    }
}
