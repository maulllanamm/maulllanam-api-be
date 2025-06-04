using System.Text.Json.Serialization;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.DTO;

public class CreateSkillDTO
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public SkillType Type { get; set; }

}