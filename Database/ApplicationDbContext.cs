using maulllanam_api_be.Entity;
using Microsoft.EntityFrameworkCore;
using File = maulllanam_api_be.Entity.File;

namespace maulllanam_api_be.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<File> Files { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.Phone)
                .HasMaxLength(20);
            entity.Property(e => e.Summary)
                .IsRequired()
                .HasMaxLength(1000);

            // BaseEntity fields
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);
        });
        
        modelBuilder.Entity<SocialMedia>(entity =>
        {
            entity.ToTable("social_medias");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Platform)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(200);
            
            // BaseEntity fields
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            // Relasi ke User
            entity.HasOne(e => e.User)
                .WithMany(u => u.SocialMedias)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("skills");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Level)
                .IsRequired()
                .HasConversion<int>(); 

            entity.Property(e => e.Type)
                .IsRequired()
                .HasConversion<int>(); 
            
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .IsRequired();

            entity.Property(e => e.Url)
                .HasMaxLength(500);
            
            entity.Property(e => e.Github)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Tech)
                .HasColumnType("jsonb"); 
            
            entity.Property(e => e.Features)
                .HasColumnType("jsonb"); 
        });
        
        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable("educations");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Institution)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Degree)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.StartYear)
                .IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("experiences");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.StartDate)
                .IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Experiences)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.ToTable("certificates");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(c => c.IssuedBy)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(c => c.Url)
                .HasMaxLength(500);

            entity.Property(c => c.DateIssued)
                .IsRequired();

            entity.HasOne(c => c.User)
                .WithMany(u => u.Certificates)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("files");

            entity.HasKey(f => f.Id);

            entity.Property(f => f.OriginalFileName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(f => f.StoredFileName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(f => f.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(f => f.FileSize)
                .IsRequired();

            entity.Property(f => f.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(f => f.FileHash)
                .HasMaxLength(128);
            
            entity.HasOne(c => c.User)
                .WithMany(u => u.Files)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(f => f.CreatedAt)
                .IsRequired();

            entity.Property(f => f.UpdatedAt)
                .IsRequired(false);

            entity.Property(f => f.IsDeleted)
                .IsRequired();
        });

    }
    
    // Override SaveChanges untuk audit trail
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IBaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (IBaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsDeleted = false;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}