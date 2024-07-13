using ControlInventoryManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract
{
    public interface IEntrepriseService
    {
        Task<IEnumerable<Entreprise>> GetAllEntreprises();
        Task<Entreprise> GetEntrepriseById(int id);
        Task<Entreprise> CreateEntreprise(Entreprise newEntreprise);
        Task UpdateEntreprise(Entreprise updatedEntreprise);
        Task DeleteEntreprise(Entreprise entreprise);
    }
}
