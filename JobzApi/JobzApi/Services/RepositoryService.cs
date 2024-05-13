using JobzApi.Data;
using JobzApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace JobzApi.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        protected ApplicationDbContext _applicationDbContext;

        public RepositoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IDbContextTransaction> GetTransactionObject()
        {
            return await _applicationDbContext.Database.BeginTransactionAsync();
        }

        public int Count()
        {
            return _applicationDbContext.Set<T>().Count();
        }

        public async Task CreateAsync(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _applicationDbContext.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _applicationDbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _applicationDbContext.Set<T>().Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
