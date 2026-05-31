using System.ComponentModel.DataAnnotations;

namespace EscapeRoomSystem.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public int TotalGamesPlayed { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}