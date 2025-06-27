using System.Net;
using System.Security.Cryptography;
using maulllanam_api_be.Database;
using maulllanam_api_be.DTO;
using Microsoft.EntityFrameworkCore;
using File = maulllanam_api_be.Entity.File;

namespace maulllanam_api_be.Service;

public class FileService : BaseService<File>, IFileService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly string _uploadPath;
    private readonly long _maxFileSize;
    private readonly string[] _allowedExtensions;

    public FileService(ApplicationDbContext context, IConfiguration configuration) : base(context)
    {
        _context = context;
        _configuration = configuration;
        _uploadPath = _configuration["FileStorage:UploadPath"] ?? "uploads";
        _maxFileSize = _configuration.GetValue<long>("FileStorage:MaxFileSize", 10485760); // 10MB default
        _allowedExtensions = _configuration.GetSection("FileStorage:AllowedExtensions").Get<string[]>() 
                             ?? new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".txt" };

        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }
    
    

    public async Task<FileUploadResponseDTO> UploadFileAsync(IFormFile file, string? uploadedBy = null)
    {

            var validation = ValidateFile(file);
            if (!validation.IsValid)
            {
                throw new ArgumentException(validation.ErrorMessage);
            }
            
            // Generate unique filename
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var storedFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_uploadPath, storedFileName);

            // Calculate file hash
            var fileHash = await CalculateFileHashAsync(file);

            // Check for duplicate files
            var existingFile = await _context.Files
                .FirstOrDefaultAsync(f => f.FileHash == fileHash && !f.IsDeleted);

            if (existingFile != null)
            {
                return new FileUploadResponseDTO
                {
                    Id = existingFile.Id,
                    OriginalFileName = existingFile.OriginalFileName,
                    ContentType = existingFile.ContentType,
                    FileSize = existingFile.FileSize,
                    UploadedAt = existingFile.CreatedAt,
                    Message = "File already exists"
                };
            }

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save to database
            var fileEntity = new File
            {
                OriginalFileName = file.FileName,
                StoredFileName = storedFileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                FilePath = filePath,
                FileHash = fileHash,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Files.Add(fileEntity);
            await _context.SaveChangesAsync();


            return new FileUploadResponseDTO
            {
                Id = fileEntity.Id,
                OriginalFileName = fileEntity.OriginalFileName,
                ContentType = fileEntity.ContentType,
                FileSize = fileEntity.FileSize,
                UploadedAt = fileEntity.CreatedAt,
                Message = "File uploaded successfully"
            };
    }

    public async Task<FileDownloadResponseDTO?> GetFileAsync(Guid fileId)
    {
        var fileEntity = await _context.Files
            .FirstOrDefaultAsync(f => f.Id == fileId && !f.IsDeleted);
       
        if (fileEntity == null || !System.IO.File.Exists(fileEntity.FilePath))
        {
            return null;
        }

        var fileStream = new FileStream(fileEntity.FilePath, FileMode.Open, FileAccess.Read);

        return new FileDownloadResponseDTO()
        {
            FileStream = fileStream,
            ContentType = fileEntity.ContentType,
            FileName = fileEntity.OriginalFileName,
            FileSize = fileEntity.FileSize
        };
    }

    private (bool IsValid, string ErrorMessage) ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return (false, "File is empty or null");
        }

        if (file.Length > _maxFileSize)
        {
            return (false, $"File size exceeds maximum allowed size of {_maxFileSize / 1024 / 1024}MB");
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(fileExtension))
        {
            return (false, $"File extension '{fileExtension}' is not allowed");
        }
        
        var allowedContentTypes = new[]
        {
            "image/jpeg", "image/jpg", "image/png", "image/gif",
            "application/pdf", "text/plain",
            "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        };

        if (!allowedContentTypes.Contains(file.ContentType.ToLowerInvariant()))
        {
            return (false, $"Content type '{file.ContentType}' is not allowed");
        }

        return (true, string.Empty);
    }
    
    private async Task<string> CalculateFileHashAsync(IFormFile file)
    {
        using var sha256 = SHA256.Create();
        using var stream = file.OpenReadStream();
        var hashBytes = await Task.Run(() => sha256.ComputeHash(stream));
        return Convert.ToBase64String(hashBytes);
    }
}