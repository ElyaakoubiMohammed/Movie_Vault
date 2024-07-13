using ControlInventoryManagment.ServicesContract.Repos;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.ServicesContract.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        ICategorieRepository Categories { get; }
        ICityRepository Cities { get; }
        IEntrepriseRepository Entreprises { get; }
        IEtageRepository Etages { get; }
        ILocalRepository Locals { get; }
        IOperationRepository Operations { get; }
        IProductRepository Products { get; }
        IStockageRepository Stockages { get; }
        ITypeEtagenRepository TypeEtages { get; }

        Task<int> CommitAsync();
    }
}
