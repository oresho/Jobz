using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace JobzApi.Services.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        int Count();
        Task<IDbContextTransaction> GetTransactionObject();
    }

}