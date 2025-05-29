using maulllanam_api_be.Database;
using maulllanam_api_be.DTO;
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

    public async Task<AboutDTO> GetAbout(Guid id)
    {
        var user = await _dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        return new AboutDTO
        {
            Name = user.Name,
            Email = user.Email,
            Summary = user.Summary,
            Title = user.Title,
            Phone = user.Phone
        };
    }
}