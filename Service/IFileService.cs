using maulllanam_api_be.DTO;
using maulllanam_api_be.Entity;
using File = maulllanam_api_be.Entity.File;

namespace maulllanam_api_be.Service;

public interface IFileService: IBaseService<File>
{
    Task<FileUploadResponseDTO> UploadFileAsync(IFormFile file, string? uploadedBy = null);
    Task<FileDownloadResponseDTO?> GetFileAsync(Guid fileId);
}