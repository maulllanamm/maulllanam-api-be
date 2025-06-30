using System.Text.Json.Serialization;

namespace maulllanam_api_be.Entity;

public class Skill : BaseEntity, IHasUserId
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public SkillType Type { get; set; }

    public int Level { get; set; } = 0;

    // Navigation property
    [JsonIgnore]
    public User User { get; set; } = null!;
}

public enum SkillType
{
    Frontend = 1,
    Backend = 2,
    DevOps = 3,
    Database = 4,
    SoftSkill = 5,
    Other = 99
}