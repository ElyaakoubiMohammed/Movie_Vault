using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IEntrepriseRepository
    {
        Task<IEnumerable<Entreprise>> GetAllEntreprise();
        Task<Entreprise> GetEntrepriseById(int id);
        Task<Entreprise> CreateEntreprise(Entreprise newEntreprise);
        Task UpdateEntreprise(Entreprise updatedEntreprise);
        Task DeleteEntreprise(Entreprise entreprise);
    }
}