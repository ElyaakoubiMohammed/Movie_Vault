using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.ServicesContract.Repos;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Categories = new CategorieRepository(_dbContext);
            Cities = new CityRepository(_dbContext);
            Entreprises = new EntrepriseRepository(_dbContext);
            Etages = new EtageRepository(_dbContext);
            Locals = new LocalRepository(_dbContext);
            Operations = new OperationRepository(_dbContext);
            Products = new ProductRepository(_dbContext);
            Stockages = new StockageRepository(_dbContext);
            TypeEtages = new TypeEtageRepository(_dbContext);
        }

        public ICategorieRepository Categories { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IEntrepriseRepository Entreprises { get; private set; }
        public IEtageRepository Etages { get; private set; }
        public ILocalRepository Locals { get; private set; }
        public IOperationRepository Operations { get; private set; }
        public IProductRepository Products { get; private set; }
        public IStockageRepository Stockages { get; private set; }
        public ITypeEtagenRepository TypeEtages { get; private set; }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error saving changes", ex);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}