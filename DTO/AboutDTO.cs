using maulllanam_api_be.Entity;

namespace maulllanam_api_be.DTO;

public class AboutDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Summary { get; set; }
    public List<SocialMediaDTO> SocialMedias { get; set; } = new List<SocialMediaDTO>();
}