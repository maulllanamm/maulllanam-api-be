namespace maulllanam_api_be.DTO;

public class CreateEducationDTO
{
    public Guid UserId { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public int StartYear { get; set; }
    public int? EndYear { get; set; } 
    public string? Description { get; set; }
}