using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IEntrepriseRepository
    {
        Task<EntrepriseReadDTO> GetEntrepriseById(int id);
        Task<EntrepriseReadDTO> GetEntrepriseByName(string name);
        Task<EntrepriseReadDTO> CreateEntreprise(EntrepriseCreateDTO newEntreprise);
        Task UpdateEntreprise(EntrepriseUpdateDTO updatedEntreprise);
        Task DeleteEntreprise(int id);
    }
}
