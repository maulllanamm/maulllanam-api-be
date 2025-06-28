using System.Text.Json.Serialization;

namespace maulllanam_api_be.Entity;

public class Experience : BaseEntity, IHasUserId
{
    public Guid UserId { get; set; }

    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
    public string? Description { get; set; }

    [JsonIgnore]
    public User User { get; set; } = null!;
}
