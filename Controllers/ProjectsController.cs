using System.Text.Json;
using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController: ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }
    
    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectById(Guid id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    
   
    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject([FromBody] CreateProjectlDTO project)
    {
        
        var projectRntity = new Project
        {
            UserId = project.UserId,
            Title = project.Title,
            Description = project.Description,
            Url = project.Url,
            Tech = JsonSerializer.Serialize(project.Tech),
            
        };
        var createdProject = await _projectService.CreateAsync(projectRntity);
        return Ok(createdProject);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Project>>> UpdateProject(Guid id, [FromBody] UpdateProjectDTO project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }
        var projectEntity = new Project
        {
            Id = id,
            UserId = project.UserId,
            Title = project.Title,
            Url = project.Url,
            Tech = JsonSerializer.Serialize(project.Tech),
            Description = project.Description,
        };
        await _projectService.UpdateAsync(projectEntity);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> SoftDeleteProject(Guid id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (id != project.Id)
        {
            return BadRequest();
        }

        if (project == null)
        {
            return NotFound();
        }

        if (project.IsDeleted)
        {
            return BadRequest();
        }
        
        
        await _projectService.DeleteAsync(id);
        return NoContent();
    }
}