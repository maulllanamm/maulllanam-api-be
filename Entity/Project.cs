using System.Text.Json.Serialization;

namespace maulllanam_api_be.Entity;

public class Project: BaseEntity, IHasUserId
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Url { get; set; }

    public string Tech { get; set; }

    [JsonIgnore]
    public User User { get; set; } = null!;
}