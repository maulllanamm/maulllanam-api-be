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

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllWithIncludeAsync(
            u => !u.IsDeleted, 
            u => u.SocialMedias, 
            u => u.Skills);
        return Ok(users);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersById(Guid id)
    {
        var user = await _userService.GetByIdWithIncludeAsync(id, u => !u.IsDeleted, u=> u.SocialMedias);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet]
    [Route("{id}/about")]
    public async Task<ActionResult<IEnumerable<User>>> GetAbout(Guid id)
    {
        var user = await _userService.GetAbout(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
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