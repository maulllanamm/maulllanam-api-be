using System.Text.Json;
using maulllanam_api_be.Entity;
using maulllanam_api_be.Helper;
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
        await SeedCertificates(db);
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
            Summary = "A highly motivated and results-driven .NET Developer with 3 years of professional experience, specializing in backend development. Proficient in building robust and scalable applications using .NET, PostgreSQL, Redis, and Docker. Comfortable working in cross-functional teams and large-scale environments, with a solid understanding of frontend development, DevOps practices, and computer networking. Passionate about continuous learning and aiming to grow into a Senior Engineer or Tech Lead role.",
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
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "HTML + CSS + JS", Level = 90, Type = SkillType.Frontend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "React", Level = 80, Type = SkillType.Frontend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = ".NET Core", Level = 90, Type = SkillType.Backend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Laravel", Level = 78, Type = SkillType.Backend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Node JS", Level = 75, Type = SkillType.Backend, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "MySQL", Level = 90, Type = SkillType.Database, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "PostgreSQL", Level = 90, Type = SkillType.Database, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Redis", Level = 85, Type = SkillType.Database, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Docker", Level = 90, Type = SkillType.DevOps, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Kubernetes", Level = 80, Type = SkillType.DevOps, CreatedAt = DateTime.UtcNow, IsDeleted = false },
                new Skill { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Github Action", Level = 90, Type = SkillType.DevOps, CreatedAt = DateTime.UtcNow, IsDeleted = false }
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
        db.Projects.AddRange(new List<Project>
        {
            new Project
            {
                Id = Guid.NewGuid(),
                UserId = _userGuid,
                Title = "BlockchainNet",
                Slug = StringHelper.GenerateSlug("BlockchainNet"),
                Description = "A simplified blockchain simulation application developed using .NET Core, designed to demonstrate the fundamental concepts of blockchain such as block creation, hashing, and chain validation.",
                Tech = JsonSerializer.Serialize(new List<string> { "React", "Tailwind", ".NET Core" }),
                Features = JsonSerializer.Serialize(new List<string>
                {
                    "Mining simulation with Proof of Work (PoW) algorithm",
                    "Block creation and hash verification",
                    "Pending transaction queue and confirmation",
                    "Key pair generation simulation (public/private key)",
                    "Wallet creation with digital signature simulation",
                    "Check wallet balance and transaction history by address"
                }),
                Github = "https://github.com/maulllanamm/BlockchainNet",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Project
            {
                Id = Guid.NewGuid(),
                UserId = _userGuid,
                Title = "Simple Todo App",
                Slug = StringHelper.GenerateSlug("Simple Todo App"),
                Description = "A basic to-do list web application built with React and Tailwind CSS, allowing users to add, edit, and delete tasks with a responsive user interface.",
                Tech = JsonSerializer.Serialize(new List<string> { "React", "Tailwind" }),
                Features = JsonSerializer.Serialize(new List<string>
                {
                    "Add tasks with priority levels",
                    "Edit and update existing tasks",
                    "Mark tasks as completed",
                    "Delete tasks from the list"
                }),
                Github = "https://github.com/maulllanamm/todo-list",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Project
            {
                Id = Guid.NewGuid(),
                UserId = _userGuid,
                Title = "MTerm",
                Slug = StringHelper.GenerateSlug("MTerm"),
                Description = "A personal portfolio website designed with a terminal-style user interface. Built with React and .NET Core 8, and integrated with PostgreSQL for structured backend data management.",
                Tech = JsonSerializer.Serialize(new List<string> { "React", "Tailwind", ".NET Core 8", "PostgreSQL" }),
                Features = JsonSerializer.Serialize(new List<string>
                {
                    "Interactive terminal-style user interface",
                    "Command-line inspired navigation (e.g., whoami, skills, projects)",
                    "Dynamic data fetching from backend",
                }),
                Github = "https://github.com/maulllanamm/mterm",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
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
                    Company = "PT ASTRA International Tbk",
                    Role = ".NET Developer",
                    StartDate = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = null,
                    Description = "Designing and developing internal enterprise tools using .NET 6 to enhance operational efficiency. Responsible for maintaining and modernizing legacy systems, implementing RESTful APIs, and collaborating with cross-functional teams to deliver scalable backend solutions aligned with business requirements.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                
                new Experience
                {
                    Id = Guid.NewGuid(),
                    UserId = _userGuid,
                    Company = "PT Beit Tiwikrama",
                    Role = "Backend Developer",
                    StartDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2024, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Developed and maintained RESTful APIs. Optimized database performance using PostgreSQL, implemented business logic in .NET Core, and integrated third-party services. Collaborated closely with frontend developers to ensure seamless system functionality and deployment.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },


                new Experience
                {
                    Id = Guid.NewGuid(),
                    UserId = _userGuid,
                    Company = "RS UMMI Bogor",
                    Role = "Backend Developer",
                    StartDate = new DateTime(2022, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2022, 11, 30, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Developed and maintained an academic information system using CodeIgniter 3. optimizing MySQL queries, and ensuring smooth backend functionality to support academic operations.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
            });

            await db.SaveChangesAsync();
        }
    }
    private static async Task SeedCertificates(ApplicationDbContext db)
    {
        if (!db.Certificates.Any())
        {
            var now = DateTime.UtcNow;

            var certificates = new List<Certificate>
            {
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Dasar Pemrograman JavaScript", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2022, 8, 31, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1OFrPm8UZYKMcp0-A1Gzg1Rgm_CRqldru/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Fundamental Aplikasi Web dengan React", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 8, 19, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1B-lzQIr44-0Ru9BjCIC6tJ68t4t_Dfso/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Dasar Pemrograman Web", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 22, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1HKApXdspwx3lrVkrX14ZHoNdr5w3dpAt/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Membuat Aplikasi Web dengan React", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2022, 9, 7, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1TtWYlWEBQduKzfFXEJFsF03M_n-aH6HJ/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Membuat Front-End Web untuk Pemula", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 23, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1rUzkyhP44xQDPdZbiHwVTyEYHZofeJKS/view?usp=share_link", CreatedAt = now, IsDeleted = false },

                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Architecting on AWS (Membangun Arsitektur Cloud di AWS)", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 28, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/133bio_QA-ISnVaZFkuWgXvIrqNP36_Zg/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Back-End Pemula dengan JavaScript", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 20, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1ZOP6j7Y1OCtKam9Poc6g73VAKAm3sVIT/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Fundamental Back-End dengan JavaScript", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 8, 1, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1fOjj7hWNQqEMy5KeL-EvwyD8O3VLBcrJ/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Prinsip Pemrograman SOLID", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2023, 7, 17, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/17NnjCHFCtS3S0gCN-E0v2lGw9gkYRTKm/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Cloud Practitioner Essentials (Belajar Dasar AWS Cloud)", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 18, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1yUwMVPJfe0B0souasl_6ID7ghkB1iZPJ/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Menjadi Node.js Application Developer", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 27, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1-eE0BNhy94gnMRUJ5M-wvA6FNYH4iGSe/view?usp=share_link", CreatedAt = now, IsDeleted = false },

                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Dasar-Dasar DevOps", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 21, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1V4YxQ9iRGfawm9JF3vlCa2d4Z4I_lVNu/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Jaringan Komputer untuk Pemula", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 23, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1K1o4pI4oZoLpo23xpcGcok6M-Pp4IBAq/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Menjadi Linux System Administrator", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 7, 25, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1hhYZ_6043vkovs3PwbhrpY5CAzcoqMl5/view?usp=share_link", CreatedAt = now, IsDeleted = false },

                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Dasar Google Cloud", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 8, 1, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1b5zA6pPuqrKFwXyQe588bxjqgnbDpGuL/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Belajar Membuat Aplikasi Back-End untuk Pemula dengan Google Cloud", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 8, 11, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1xk_exDmHUf7b5rN1vdf6dUcmIa5zV6z3/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Menjadi Google Cloud Engineer", IssuedBy = "Dicoding Indonesia", DateIssued = new DateTime(2024, 8, 16, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1C-KfnqwfPFVGRniTRvNQSeIzlJXarcvf/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Elementary Levels", IssuedBy = "Lembaga Bahasa LIA", DateIssued = new DateTime(2018, 3, 23, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1zBQqdCt65oPJDhjz-NXVzd8hWlmAChu4/view?usp=share_link", CreatedAt = now, IsDeleted = false },
                new Certificate { Id = Guid.NewGuid(), UserId = _userGuid, Name = "Intermediate Levels", IssuedBy = "Lembaga Bahasa LIA", DateIssued = new DateTime(2019, 12, 19, 0, 0, 0, DateTimeKind.Utc), Url = "https://drive.google.com/file/d/1C1A14Z0_gTGAPnjIjb3YxBdH7e5aGtbg/view?usp=share_link", CreatedAt = now, IsDeleted = false }
            };

            db.Certificates.AddRange(certificates);
            await db.SaveChangesAsync();
        }
    }

}






