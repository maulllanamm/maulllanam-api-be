using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISocialMediaService _socialMediaService;
    private readonly ISkillService _skillService;
    private readonly IProjectService _projectService;
    private readonly IExperienceService _experienceService;
    private readonly IEducationService _educationService;
    private readonly ICertificateService _certificateService;
    private readonly IFileService _fileService;
    public UsersController(
        IUserService userService, 
        ISocialMediaService socialMediaService, 
        ISkillService skillService, 
        IProjectService projectService, 
        IExperienceService experienceService, 
        IEducationService educationService, 
        ICertificateService certificateService, 
        IFileService fileService)
    {
        _userService = userService;
        _socialMediaService = socialMediaService;
        _skillService = skillService;
        _projectService = projectService;
        _experienceService = experienceService;
        _educationService = educationService;
        _certificateService = certificateService;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllWithIncludeAsync(
            u => !u.IsDeleted, 
            u => u.SocialMedias, 
            u => u.Skills,
            u => u.Projects,
            u => u.Educations,
            u => u.Experiences,
            u => u.Certificates);
        return Ok(users);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersById(Guid id)
    {
        var user = await _userService.GetByIdWithIncludeAsync(id, 
            u => !u.IsDeleted, 
            u=> u.SocialMedias,
            u => u.Skills,
            u => u.Projects,
            u => u.Educations,
            u => u.Experiences,
            u => u.Certificates);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet]
    [Route("{userId}/about")]
    public async Task<ActionResult<IEnumerable<User>>> GetAbout(Guid userId)
    {
        var user = await _userService.GetAbout(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet]
    [Route("{userId}/social-medias")]
    public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMediaByUserId(Guid userId)
    {
        var socialMedia = await _socialMediaService.GetByUserIdAsync<SocialMedia>(userId);
        if (!socialMedia.Any())
        {
            return NotFound();
        }
        return Ok(socialMedia);
    }
    
    [HttpGet]
    [Route("{userId}/skills")]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkillByUserId(Guid userId)
    {
        var skills = await _skillService.GetByUserIdAsync<Skill>(userId);
        if (!skills.Any())
        {
            return NotFound();
        }
        return Ok(skills);
    }
    
    [HttpGet]
    [Route("{userId}/projects")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectByUserId(Guid userId)
    {
        var project = await _projectService.GetByUserIdAsync<Project>(userId);
        if (!project.Any())
        {
            return NotFound();
        }
        return Ok(project);
    }
    
    [HttpGet]
    [Route("{userId}/experiences")]
    public async Task<ActionResult<IEnumerable<Experience>>> GetExperienceByUserId(Guid userId)
    {
        var experience = await _experienceService.GetByUserIdAsync<Experience>(userId);
        if (!experience.Any())
        {
            return NotFound();
        }
        return Ok(experience);
    }
    
    [HttpGet]
    [Route("{userId}/educations")]
    public async Task<ActionResult<IEnumerable<Education>>> GetEducationByUserId(Guid userId)
    {
        var education = await _educationService.GetByUserIdAsync<Education>(userId);
        if (!education.Any())
        {
            return NotFound();
        }
        return Ok(education);
    }
    
    [HttpGet]
    [Route("{userId}/certificates")]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificateByUserId(Guid userId)
    {
        var certificate = await _certificateService.GetByUserIdAsync<Certificate>(userId);
        if (!certificate.Any())
        {
            return NotFound();
        }
        return Ok(certificate);
    }
    
    [HttpGet("{userId}/cv/download")]
    public async Task<IActionResult> DownloadFile(Guid userId)
    {
        try
        {
            var result = await _fileService.GetFileByUserIdAsync(userId);
                
            if (result == null)
            {
                return NotFound("File not found");
            }

            return File(result.FileStream, result.ContentType, result.FileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error occurred while downloading file");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDTO user)
    {
        var userEntity = new User
        {
            Name = user.Name,
            Title = user.Title,
            Email = user.Email,
            Phone = user.Phone,
            Summary = user.Summary
        };
        var createdUser = await _userService.CreateAsync(userEntity);
        return Ok(createdUser);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> UpdateUser(Guid id, [FromBody] UpdateUserDTO user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        var userEntity = new User
        {
            Id = id,
            Name = user.Name,
            Title = user.Title,
            Email = user.Email,
            Phone = user.Phone,
            Summary = user.Summary
        };
        await _userService.UpdateAsync(userEntity);
        return NoContent();
    }   
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> SoftDeleteUser(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (id != user.Id)
        {
            return BadRequest();
        }

        if (user == null)
        {
            return NotFound();
        }

        if (user.IsDeleted)
        {
            return BadRequest();
        }
        
        
        await _userService.DeleteAsync(id);
        return NoContent();
    }
   
}