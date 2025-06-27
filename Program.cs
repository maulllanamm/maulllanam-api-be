using maulllanam_api_be.Database;
using maulllanam_api_be.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

// Configure request size limits
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 104857600; // 100MB
});

// 4. Add controllers and API services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// 5. Apply database migrations (optional - hanya jika diperlukan)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        logger.LogInformation("Memulai proses migrasi database...");
        
        // Cek apakah ada migrasi yang pending
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Ditemukan {pendingMigrations.Count()} migrasi pending: {string.Join(", ", pendingMigrations)}");
            
            // Jalankan migrasi
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrasi database berhasil!");
        }
        else
        {
            logger.LogInformation("Tidak ada migrasi pending. Database sudah up to date.");
        }
        
        // Cek migrasi yang sudah diterapkan
        var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
        logger.LogInformation($"Migrasi yang sudah diterapkan: {string.Join(", ", appliedMigrations)}");
        
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "GAGAL menjalankan migrasi database: {Message}", ex.Message);
        // JANGAN throw exception di sini untuk debugging
        // throw; // Comment ini dulu
    }
}

// Panggil seeder
try 
{
    await DbSeeder.SeedAsync(app.Services);
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error saat menjalankan seeder: {Message}", ex.Message);
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aktifkan CORS
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();