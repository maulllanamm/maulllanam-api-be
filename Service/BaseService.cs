using System.Linq.Expressions;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class BaseService<T> : IBaseService<T> where T : IBaseEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseService(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public virtual async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> condition)
    {
        return await _dbSet
            .Where(x => !x.IsDeleted)
            .Where(condition)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsDeleted = false;

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }


}