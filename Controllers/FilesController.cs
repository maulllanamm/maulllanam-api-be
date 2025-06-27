using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController: ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        try
        {
            var result = await _fileService.GetFileAsync(id);
                
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
    
    [HttpPost("upload")]
    [RequestSizeLimit(10485760)] // 10MB
    public async Task<ActionResult<FileUploadResponseDTO>> UploadFile(IFormFile file)
    {
        try
        {
            if (file == null)
            {
                return BadRequest("No file provided");
            }

            var result = await _fileService.UploadFileAsync(file);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error occurred while uploading file");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(Guid id)
    {
        try
        {
            var result = await _fileService.DeleteFileAsync(id);
                
            if (!result)
            {
                return NotFound("File not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error occurred while deleting file");
        }
    }

}