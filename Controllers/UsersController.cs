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
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersById(Guid id)
    {
        var users = await _userService.GetByIdAsync(id);
        return Ok(users);
    }
    
    [HttpGet]
    [Route("{id}/about")]
    public async Task<ActionResult<IEnumerable<User>>> GetAbout(Guid id)
    {
        var users = await _userService.GetByIdAsync(id);
        return Ok(users);
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
    
    [HttpPut]
    public async Task<ActionResult<IEnumerable<User>>> UpdateUser([FromBody] User user)
    {
        var users = await _userService.UpdateAsync(user);
        return Ok(users);
    }
   
}