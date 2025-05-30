using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.Service;

public class SocialMediaService :  BaseService<SocialMedia>, ISocialMediaService
{
    private readonly ApplicationDbContext _context;
    public SocialMediaService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}