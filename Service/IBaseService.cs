using System.Linq.Expressions;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.Service;

public interface IBaseService<T> where T : IBaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<List<T>> GetAllWithIncludeAsync(
        Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> condition);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> SoftDeleteAsync(Guid id);
}