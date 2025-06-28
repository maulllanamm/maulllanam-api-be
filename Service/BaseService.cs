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
    
    public virtual async Task<List<T>> GetAllWithIncludeAsync(
        Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetByUserIdAsync<TUserEntity>(Guid userId)
        where TUserEntity : class, T, IHasUserId
    {
        return await _context.Set<TUserEntity>()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .Cast<T>()
            .ToListAsync();
    }

    
    public virtual async Task<T> GetByIdWithIncludeAsync(
        Guid id,
        Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Tambahkan includes
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // Gabungkan filter dengan id dan IsDeleted
        Expression<Func<T, bool>> finalFilter = x => x.Id == id && !x.IsDeleted;

        if (filter != null)
        {
            // Gabungkan filter kustom dan filter bawaan (id dan IsDeleted)
            var parameter = Expression.Parameter(typeof(T));
            var combined = Expression.AndAlso(
                Expression.Invoke(finalFilter, parameter),
                Expression.Invoke(filter, parameter)
            );
            finalFilter = Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        return await query.FirstOrDefaultAsync(finalFilter);
    }



    public virtual async Task<T?> GetByIdAsync(Guid id)
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

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"Entity with ID {entity.Id} not found");
        }

        entity.UpdatedAt = DateTime.UtcNow;
        entity.CreatedAt = existingEntity.CreatedAt; 

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> SoftDeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            return false;
        }

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}