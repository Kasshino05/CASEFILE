using System.ComponentModel.DataAnnotations;

namespace CASEFILE.Models;

public class Case
{
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string ReferenceNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Open";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}