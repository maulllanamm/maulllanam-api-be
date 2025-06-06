using System.Text.Json.Serialization;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.DTO;

public class CreateProjectlDTO
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Url { get; set; }

    public List<string> Tech { get; set; } = new();

}