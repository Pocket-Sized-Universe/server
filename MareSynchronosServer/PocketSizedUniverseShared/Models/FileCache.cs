using System.ComponentModel.DataAnnotations;

namespace PocketSizedUniverseShared.Models;

public class FileCache
{
    [Key]
    [MaxLength(40)]
    public string Hash { get; set; }
    [MaxLength(10)]
    public string UploaderUID { get; set; }
    public User Uploader { get; set; }
    public bool Uploaded { get; set; }
    public DateTime UploadDate { get; set; }
    [Timestamp]
    public byte[] Timestamp { get; set; }
    public long Size { get; set; }
    public long RawSize { get; set; }
    [MaxLength(2048)]
    public string? MagnetLink { get; set; }
    public bool IsForbidden { get; set; } = false;
}
