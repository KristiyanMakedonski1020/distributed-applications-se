using System.ComponentModel.DataAnnotations;

namespace EscapeRoomSystem.Models
{
    public class EscapeRoom
    {
        [Key] 
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)] 
        public string Name { get; set; } = string.Empty;

        [Range(1, 10)] 
        public int DifficultyLevel { get; set; }

        public decimal PricePerPerson { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpeningDate { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}