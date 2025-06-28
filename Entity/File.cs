using System.Text.Json.Serialization;

namespace maulllanam_api_be.Entity;

public class File : BaseEntity, IHasUserId
{
    public Guid UserId { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;

    public string StoredFileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;
        
    public long FileSize { get; set; }
        
    public string FilePath { get; set; } = string.Empty;
        
    public string? FileHash { get; set; }
            
    [JsonIgnore]
    public User User { get; set; } = null!;
}