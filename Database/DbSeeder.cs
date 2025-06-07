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

 
}
