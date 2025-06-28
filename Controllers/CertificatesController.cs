using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/certificates")]
public class CertificatesController: ControllerBase
{
    private readonly ICertificateService _service;

    public CertificatesController(ICertificateService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificate()
    {
        var certificates = await _service.GetAllAsync();
        return Ok(certificates);
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificateById(Guid id)
    {
        var certificate = await _service.GetByIdAsync(id);
        if (certificate == null)
        {
            return NotFound();
        }
        return Ok(certificate);
    }   
}