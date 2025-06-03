using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillController: ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
    {
        var skills = await _skillService.GetAllAsync();
        return Ok(skills);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkillById(Guid id)
    {
        var skill = await _skillService.GetByIdAsync(id);
        if (skill == null)
        {
            return NotFound();
        }
        return Ok(skill);
    }
    
    [HttpGet]
    [Route("{id}/user")]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkillByUserId(Guid userId)
    {
        var skills = await _skillService.GetBySkillUserId(userId);
        if (!skills.Any())
        {
            return NotFound();
        }
        return Ok(skills);
    }
    

   
}