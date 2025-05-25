using System.Linq.Expressions;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.Service;

public interface IBaseService<T> where T : IBaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
}