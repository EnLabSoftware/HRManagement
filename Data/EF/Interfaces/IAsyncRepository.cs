using Business.Base;
using System.Linq.Expressions;

namespace Data.EF.Interfaces
{
    public interface IAsyncRepository<T> where T : RootEntity
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    }
}