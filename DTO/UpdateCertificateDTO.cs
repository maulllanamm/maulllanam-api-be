namespace maulllanam_api_be.DTO;

public class UpdateCertificateDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string IssuedBy { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    public string? Url { get; set; }
}