using System.ComponentModel.DataAnnotations;
using server_side.Entities;

namespace server_side.Models;

public class Drawing
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; } = null!;

    [Required]
    public string DrawingName { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string ImageUrl { get; set; }
    public int? Rating { get; set; }
}
