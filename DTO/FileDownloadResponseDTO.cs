namespace maulllanam_api_be.DTO;

public class FileDownloadResponseDTO
{
    public Stream FileStream { get; set; } = null!;
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
}