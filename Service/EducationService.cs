using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class EducationService :  BaseService<Education>, IEducationService
{
    private readonly ApplicationDbContext _context;

    public EducationService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
}
