using System.Text.Json;
using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;

namespace maulllanam_api_be.Database;

public static class DbSeeder
{
    private static Guid _userGuid;

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await SeedUsers(db);
        
        var user = await db.Users.FirstAsync(u => u.Email == "maulllanamuhammad@gmail.com");
        _userGuid = user.Id;
        
        await SeedSkills(db);
        await SeedSocialMedias(db);
        await SeedProjects(db);
        await SeedEducations(db);
        await SeedExperiences(db);
    }

    private static async Task SeedUsers(ApplicationDbContext db)
    {
        var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Email == "maulllanamuhammad@gmail.com");

        if (existingUser != null)
        {
            _userGuid = existingUser.Id;
            return;
        }

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Name = "Maulana Muhammad",
            Email = "maulllanamuhammad@gmail.com",
            Title = ".NET Developer",
            Phone = "081395007665",
            Summary = "Hallo, Nama saya Maulana Muhammad, seorang .NET Developer yang sudah memiliki pengalaman selama lebih dari 2 tahun",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        db.Users.Add(newUser);
        await db.SaveChangesAsync();

        _userGuid = newUser.Id;
    }

    private static async Task SeedSkills(ApplicationDbContext db)
    {
        if (!db.Skills.Any())
        {
            db.Skills.AddRange(new List<Skill>
            {
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "HTML + CSS + JS", Type = SkillType.Frontend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "React", Type = SkillType.Frontend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = ".NET Core", Type = SkillType.Backend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Laravel", Type = SkillType.Backend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Node JS", Type = SkillType.Backend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "MySQL", Type = SkillType.Database, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "PostgreSQL", Type = SkillType.Database, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Redis", Type = SkillType.Database, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Docker", Type = SkillType.DevOps, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Kubernetes", Type = SkillType.DevOps, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Github Action", Type = SkillType.DevOps, CreatedAt = DateTime.UtcNow, IsDeleted = false }
            });

            await db.SaveChangesAsync();
        }
    }

    private static async Task SeedSocialMedias(ApplicationDbContext db)
    {
        if (!db.SocialMedias.Any())
        {
            db.SocialMedias.AddRange(new List<SocialMedia>
            {
                new SocialMedia { Id = Guid.NewGuid(), UserId = _userGuid, Platform = "LinkedIn", Url = "https://www.linkedin.com/in/maulanamuhammad1/", CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new SocialMedia { Id = Guid.NewGuid(), UserId = _userGuid, Platform = "Github", Url = "https://github.com/maulllanamm", CreatedAt = DateTime.UtcNow, IsDeleted = false }
            });

            await db.SaveChangesAsync();
        }
    }
    
    private static async Task SeedProjects(ApplicationDbContext db)
    {
        if (!db.Projects.Any())
        {
            var tech = new List<string> { "React", "Tailwind", ".NET Core" };
            db.Projects.AddRange(new List<Project>
            {
                new Project { Id = Guid.NewGuid(), UserId = _userGuid, Title = "BlockchainNet", Description = "Aplikasi simulasi blockchain sederhana yang dibuat dengan .NET Core", Tech = JsonSerializer.Serialize(tech) ,CreatedAt = DateTime.UtcNow, IsDeleted = false },
            });

            await db.SaveChangesAsync();
        }
    }
    private static async Task SeedEducations(ApplicationDbContext db)
    {
        if (!db.Educations.Any())
        {
            db.Educations.Add(new Education
            {
                Id = Guid.NewGuid(),
                UserId = _userGuid,
                Institution = "Pakuan University",
                Degree = "Bachelor of Computer Science",
                StartYear = 2017,
                EndYear = 2023,
                Description = "Graduated with GPA 3.94",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            });

            await db.SaveChangesAsync();
        }
    }
    
    private static async Task SeedExperiences(ApplicationDbContext db)
    {
        if (!db.Experiences.Any())
        {
            db.Experiences.AddRange(new List<Experience>
            {
                new Experience
                {
                    Id = Guid.NewGuid(),
                    UserId = _userGuid,
                    Company = "PT Beit Tiwikrama",
                    Role = "Backend Developer",
                    StartDate = new DateTime(2023, 1, 1),
                    EndDate = new DateTime(2024, 9, 30),
                    Description = "Worked on RESTful API development, database optimization, and system integration.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Experience
                {
                    Id = Guid.NewGuid(),
                    UserId = _userGuid,
                    Company = "PT ASTRA International TBK",
                    Role = ".NET Developer",
                    StartDate = new DateTime(2024, 10, 1),
                    Description = "Developed internal tools using .NET, and maintained legacy systems.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                }
            });

            await db.SaveChangesAsync();
        }
    }


}
