using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/social-media")]
public class SocialMediaController: ControllerBase
{
    private readonly ISocialMediaService _socialMediaService;

    public SocialMediaController(ISocialMediaService socialMediaService)
    {
        _socialMediaService = socialMediaService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias()
    {
        var users = await _socialMediaService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMediasById(Guid id)
    {
        var user = await _socialMediaService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet ("{userId}/users")]
    public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMediasUserById(Guid userId)
    {
        var user = await _socialMediaService.GetByConditionAsync(x => !x.IsDeleted && x.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<SocialMedia>> CreateSocialMedia([FromBody] CreateSocialMediaDTO socialMediaDto)
    {
        var socialMedia = new SocialMedia
        {
            UserId = socialMediaDto.UserId,
            Platform = socialMediaDto.Platform,
            Url = socialMediaDto.Url,
        };
        var createdSocialMedia = await _socialMediaService.CreateAsync(socialMedia);
        return Ok(createdSocialMedia);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<SocialMedia>>> UpdateUser(Guid id, [FromBody] UpdateSocialMediaDTO socialMediaDto)
    {
        if (id != socialMediaDto.Id)
        {
            return BadRequest();
        }
        var socialMediaEntity = new SocialMedia
        {
            Id = id,
            Platform = socialMediaDto.Platform,
            Url = socialMediaDto.Url,
        };
        await _socialMediaService.UpdateAsync(socialMediaEntity);
        return NoContent();
    }
   
}