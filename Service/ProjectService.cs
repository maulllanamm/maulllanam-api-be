using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class ProjectService :  BaseService<Project>, IProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
