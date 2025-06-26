using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class ExperienceService :  BaseService<Experience>, IExperienceService
{
    private readonly ApplicationDbContext _context;

    public ExperienceService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
}
