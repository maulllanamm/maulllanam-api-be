using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/experiences")]
public class ExperienceController: ControllerBase
{
   private readonly IExperienceService _experienceService;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
    {
        var experience = await _experienceService.GetAllAsync();
        return Ok(experience);
    }
    
  
}