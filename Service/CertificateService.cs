using maulllanam_api_be.Database;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Service;

public class CertificateService :  BaseService<Certificate>, ICertificateService
{
    private readonly ApplicationDbContext _context;

    public CertificateService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
}
