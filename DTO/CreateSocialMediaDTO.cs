namespace maulllanam_api_be.DTO;

public class CreateSocialMediaDTO
{
    public Guid UserId { get; set; } 
    public string Platform { get; set; } = string.Empty; 
    public string Url { get; set; } = string.Empty;
}