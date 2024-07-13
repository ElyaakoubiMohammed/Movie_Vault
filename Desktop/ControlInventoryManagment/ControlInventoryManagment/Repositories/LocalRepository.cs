using ControlInventoryManagment.DatabaseContext;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly AppDbContext _dbContext;

        public LocalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Local>> GetAlllocal()
        {
            return await _dbContext.Locals.ToListAsync();
        }

        public async Task<Local> GetlocalById(int id)
        {
            return await _dbContext.Locals.FindAsync(id);
        }

        public async Task<Local> Createlocal(Local newlocal)
        {
            await _dbContext.Locals.AddAsync(newlocal);
            await _dbContext.SaveChangesAsync();
            return newlocal;
        }

        public async Task UpdateLocal(Local updatedlocal)
        {
            _dbContext.Locals.Update(updatedlocal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Deletelocal(Local local)
        {
            _dbContext.Locals.Remove(local);
            await _dbContext.SaveChangesAsync();
        }
    }
}
