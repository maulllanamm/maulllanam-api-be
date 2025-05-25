using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class UserService : BaseService<User>, IUserService
{
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
    }
}