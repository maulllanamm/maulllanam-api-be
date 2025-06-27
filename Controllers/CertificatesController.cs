using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/certificates")]
public class CertificatesController: ControllerBase
{
    private readonly ICertificatesService _service;

    public CertificatesController(ICertificatesService service)
    {
        _service = service;
    }
}