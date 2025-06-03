using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class SkillService :  BaseService<Skill>, ISkillService
{
    private readonly ApplicationDbContext _context;

    public SkillService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetBySkillUserId(Guid userId)
    {
        return await _dbSet
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .ToListAsync();
    }
}
