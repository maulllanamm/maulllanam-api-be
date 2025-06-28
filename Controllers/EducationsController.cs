using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/educations")]
public class EducationsController: ControllerBase
{
    private readonly IEducationService _service;

    public EducationsController(IEducationService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Education>>> GetEducations()
    {
        var educations = await _service.GetAllAsync();
        return Ok(educations);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<Education>>> GetEducationById(Guid id)
    {
        var education = await _service.GetByIdAsync(id);
        if (education == null)
        {
            return NotFound();
        }
        return Ok(education);
    }
    
    [HttpGet]
    [Route("{id}/user")]
    public async Task<ActionResult<IEnumerable<Education>>> GetEducationByUserId(Guid id)
    {
        var education = await _service.GetByUserIdAsync<Education>(id);
        if (!education.Any())
        {
            return NotFound();
        }
        return Ok(education);
    }
    
    [HttpPost]
    public async Task<ActionResult<Education>> CreateEducation([FromBody] CreateEducationDTO education)
    {
        
        var educationEntity = new Education
        {
            UserId = education.UserId,
            Degree = education.Degree,
            Institution = education.Institution,
            StartYear = education.StartYear,
            EndYear = education.EndYear,
            Description = education.Description,
        };
        var createdEducation = await _service.CreateAsync(educationEntity);
        return Ok(createdEducation);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Education>>> UpdateEducation(Guid id, [FromBody] UpdateEducationDTO education)
    {
        if (id != education.Id)
        {
            return BadRequest();
        }
        var educationEntity = new Education
        {
            Id = id,
            UserId = education.UserId,
            Degree = education.Degree,
            Institution = education.Institution,
            StartYear = education.StartYear,
            EndYear = education.EndYear,
            Description = education.Description,
        };
        await _service.UpdateAsync(educationEntity);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> SoftDeleteEducation(Guid id)
    {
        var education = await _service.GetByIdAsync(id);
        if (id != education.Id)
        {
            return BadRequest();
        }

        if (education == null)
        {
            return NotFound();
        }

        if (education.IsDeleted)
        {
            return BadRequest();
        }
        
        
        await _service.DeleteAsync(id);
        return NoContent();
    }
}