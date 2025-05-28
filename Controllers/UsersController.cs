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
    public async Task<ActionResult<IEnumerable<User>>> GetUsersById([FromQuery]Guid id)
    {
        var users = await _userService.GetByIdAsync(id);
        return Ok(users);
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userService.CreateAsync(user);
        return Ok(createdUser);
    }
    
    [HttpPut]
    public async Task<ActionResult<IEnumerable<User>>> UpdateUser([FromBody] User user)
    {
        var users = await _userService.UpdateAsync(user);
        return Ok(users);
    }
   
}