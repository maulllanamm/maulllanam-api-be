namespace maulllanam_api_be.Entity;

public class File : BaseEntity
{

    public string OriginalFileName { get; set; } = string.Empty;

    public string StoredFileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;
        
    public long FileSize { get; set; }
        
    public string FilePath { get; set; } = string.Empty;
        
    public string? FileHash { get; set; }
        
}