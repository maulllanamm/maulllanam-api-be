using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class CertificatesService :  BaseService<Certificate>, ICertificatesService
{
    private readonly ApplicationDbContext _context;

    public CertificatesService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
}
