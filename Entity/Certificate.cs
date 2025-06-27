using System.Text.Json.Serialization;

namespace maulllanam_api_be.Entity;

public class Certificate : BaseEntity
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string IssuedBy { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    public string? Url { get; set; }

    [JsonIgnore]
    public User User { get; set; } = null!;
}
