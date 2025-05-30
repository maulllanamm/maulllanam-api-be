namespace maulllanam_api_be.Entity;

public class SocialMedia: BaseEntity
{
    public Guid UserId { get; set; } 
    public string Platform { get; set; } = string.Empty; 
    public string Url { get; set; } = string.Empty;

    // Navigation property
    public User User { get; set; } = null!;
}