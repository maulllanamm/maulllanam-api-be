using maulllanam_api_be.Entity;

namespace maulllanam_api_be.DTO;

public class UpdateSkillDTO
{
    public Guid Id { get; set; } 
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }

    public SkillType Type { get; set; }
}