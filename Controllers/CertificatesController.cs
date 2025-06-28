using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Service;
using Microsoft.AspNetCore.Mvc;

namespace maulllanam_api_be.Controllers;

[ApiController]
[Route("api/certificates")]
public class CertificatesController: ControllerBase
{
    private readonly ICertificateService _service;

    public CertificatesController(ICertificateService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificate()
    {
        var certificates = await _service.GetAllAsync();
        return Ok(certificates);
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificateById(Guid id)
    {
        var certificate = await _service.GetByIdAsync(id);
        if (certificate == null)
        {
            return NotFound();
        }
        return Ok(certificate);
    }   
    
    [HttpGet]
    [Route("{id}/user")]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificateByUserId(Guid id)
    {
        var certificate = await _service.GetByUserIdAsync<Certificate>(id);
        if (!certificate.Any())
        {
            return NotFound();
        }
        return Ok(certificate);
    }
    
    [HttpPost]
    public async Task<ActionResult<Certificate>> CreateCertificate([FromBody] CreateCertificateDTO certificate)
    {
        
        var certificateEntity = new Certificate
        {
            UserId = certificate.UserId,
            Name = certificate.Name,
            IssuedBy = certificate.IssuedBy,
            DateIssued = certificate.DateIssued,
            Url = certificate.Url,
        };
        var createdCertificate = await _service.CreateAsync(certificateEntity);
        return Ok(createdCertificate);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<Certificate>>> UpdateCertificate(Guid id, [FromBody] UpdateCertificateDTO certificate)
    {
        if (id != certificate.Id)
        {
            return BadRequest();
        }
        var certificateEntity = new Certificate
        {
            Id = id,
            UserId = certificate.UserId,
            Name = certificate.Name,
            IssuedBy = certificate.IssuedBy,
            DateIssued = certificate.DateIssued,
            Url = certificate.Url,
        };
        await _service.UpdateAsync(certificateEntity);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> SoftDeleteCertificate(Guid id)
    {
        var certificate = await _service.GetByIdAsync(id);
        if (id != certificate.Id)
        {
            return BadRequest();
        }

        if (certificate == null)
        {
            return NotFound();
        }

        if (certificate.IsDeleted)
        {
            return BadRequest();
        }
        
        
        await _service.DeleteAsync(id);
        return NoContent();
    }
}