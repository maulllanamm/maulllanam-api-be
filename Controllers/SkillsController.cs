using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController: ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
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
        var skills = await _skillService.GetByUserIdAsync<Skill>(userId);
        if (!skills.Any())
        {
            return NotFound();
        }
        return Ok(skills);
    }

    
    [HttpPost]
    public async Task<ActionResult<Skill>> CreateSkill([FromBody] CreateSkillDTO skill)
    {
        
        var skillEntity = new Skill
        {
            Name = skill.Name,
            UserId = skill.UserId,
            Type = skill.Type,
        };
        var createdSkill = await _skillService.CreateAsync(skillEntity);
        return Ok(createdSkill);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Skill>>> UpdateSkill(Guid id, [FromBody] UpdateSkillDTO skillDto)
    {
        if (id != skillDto.Id)
        {
            return BadRequest();
        }
        var skillEntity = new Skill
        {
            Id = id,
            Type = skillDto.Type,
            UserId = skillDto.UserId,
            Name = skillDto.Name,
        };
        await _skillService.UpdateAsync(skillEntity);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> SoftDeleteSkill(Guid id)
    {
        var skill = await _skillService.GetByIdAsync(id);
        if (id != skill.Id)
        {
            return BadRequest();
        }

        if (skill == null)
        {
            return NotFound();
        }

        if (skill.IsDeleted)
        {
            return BadRequest();
        }
        
        
        await _skillService.DeleteAsync(id);
        return NoContent();
    }
   
}