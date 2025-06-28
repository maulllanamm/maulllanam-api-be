using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/experiences")]
public class ExperiencesController: ControllerBase
{
    private readonly IExperienceService _experienceService;

    public ExperiencesController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
    {
        var experience = await _experienceService.GetAllAsync();
        return Ok(experience);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<Experience>>> GetExperienceById(Guid id)
    {
        var experience = await _experienceService.GetByIdAsync(id);
        if (experience == null)
        {
            return NotFound();
        }
        return Ok(experience);
    }
    
    [HttpGet]
    [Route("{id}/user")]
    public async Task<ActionResult<IEnumerable<Experience>>> GetExperienceByUserId(Guid id)
    {
        var experience = await _experienceService.GetByUserIdAsync<Experience>(id);
        if (!experience.Any())
        {
            return NotFound();
        }
        return Ok(experience);
    }
  
    [HttpPost]
    public async Task<ActionResult<Experience>> CreateExperience([FromBody] CreateExperienceDTO experience)
    {
        
        var experienceEntity = new Experience
        {
            UserId = experience.UserId,
            Company = experience.Company,
            Role = experience.Role,
            StartDate = experience.StartDate,
            EndDate  = experience.EndDate,
            Description = experience.Description,
        };
        var createdExperience = await _experienceService.CreateAsync(experienceEntity);
        return Ok(createdExperience);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Experience>>> UpdateExperience(Guid id, [FromBody] UpdateExperienceDTO experience)
    {
        if (id != experience.Id)
        {
            return BadRequest();
        }
        var experienceEntity = new Experience
        {
            Id = id,
            UserId = experience.UserId,
            Company = experience.Company,
            Role = experience.Role,
            StartDate = experience.StartDate,
            EndDate  = experience.EndDate,
            Description = experience.Description,
        };
        await _experienceService.UpdateAsync(experienceEntity);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> SoftDeleteExperience(Guid id)
    {
        var experience = await _experienceService.GetByIdAsync(id);
        if (id != experience.Id)
        {
            return BadRequest();
        }

        if (experience == null)
        {
            return NotFound();
        }

        if (experience.IsDeleted)
        {
            return BadRequest();
        }
        
        
        await _experienceService.DeleteAsync(id);
        return NoContent();
    }
}