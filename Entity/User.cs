namespace maulllanam_api_be.Entity;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Summary { get; set; }
    
    
    public ICollection<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    public ICollection<File> Files { get; set; } = new List<File>();


}