using System.ComponentModel.DataAnnotations;

namespace server_side.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public ICollection<Challenge> ChallengesCreated { get; set; }

    }

}