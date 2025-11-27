namespace HRManagementSystem.Application.DTOs.File;

public class FileExportDto
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] FileBytes { get; set; }
}


