using System.Text.Json;
using maulllanam_api_be.Entity;

namespace maulllanam_api_be.Database;

public static class DbSeeder
{
    private static readonly Guid _userGuid = Guid.NewGuid();

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await SeedUsers(db);
        await SeedSkills(db);

    }

    private static async Task SeedUsers(ApplicationDbContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.Add(new User
            {
                Id = _userGuid,
                Name = "Maulana Muhammad",
                Email = "maulllanamuhammad@gmail.com",
                Title = ".NET Developer",
                Phone = "081395007665",
                Summary = "Hallo, Nama saya Maulana Muhammad, seorang .NET Developer yang sudah memiliki pengalaman selama lebih dari 2 tahun",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            });

            await db.SaveChangesAsync();
        }
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


}
