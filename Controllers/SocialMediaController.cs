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
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateSocialMedia([FromBody] CreateSocialMediaDTO socialMediaDto)
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
   
}