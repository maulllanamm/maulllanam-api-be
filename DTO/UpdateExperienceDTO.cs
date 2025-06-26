namespace maulllanam_api_be.DTO;

public class UpdateExperienceDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
    public string? Description { get; set; }
}