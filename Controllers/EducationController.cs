using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/educations")]
public class EducationController: ControllerBase
{
    private readonly IEducationService _service;

    public EducationController(IEducationService service)
    {
        _service = service;
    }
}