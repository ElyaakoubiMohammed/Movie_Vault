using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.ServicesContract.Repos;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UnitOfWork(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            Categories = new CategorieRepository(_dbContext, _mapper);
            Cities = new CityRepository(_dbContext, _mapper);
            Entreprises = new EntrepriseRepository(_dbContext, _mapper);
            Etages = new EtageRepository(_dbContext, _mapper);
            Locals = new LocalRepository(_dbContext, _mapper);
            Operations = new OperationRepository(_dbContext, _mapper);
            Products = new ProductRepository(_dbContext, _mapper);
            Stockages = new StockageRepository(_dbContext, _mapper);
            TypeEtages = new TypeEtageRepository(_dbContext, _mapper);
        }

        public ICategorieRepository Categories { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IEntrepriseRepository Entreprises { get; private set; }
        public IEtageRepository Etages { get; private set; }
        public ILocalRepository Locals { get; private set; }
        public IOperationRepository Operations { get; private set; }
        public IProductRepository Products { get; private set; }
        public IStockageRepository Stockages { get; private set; }
        public ITypeEtageRepository TypeEtages { get; private set; }

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
