using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using File = maulllanam_api_be.Entity.File;

namespace maulllanam_api_be.Service;

public interface IFileService: IBaseService<File>
{
    Task<IEnumerable<FileDTO>> GetAllAsync();
    Task<FileUploadResponseDTO> UploadFileAsync(IFormFile file, Guid userId);
    Task<FileDownloadResponseDTO?> GetFileByUserIdAsync(Guid userId);
    Task<FileDownloadResponseDTO?> GetFileAsync(Guid fileId);
    Task<bool> DeleteFileAsync(Guid fileId);
}