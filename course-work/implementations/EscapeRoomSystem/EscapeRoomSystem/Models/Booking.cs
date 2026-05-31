using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscapeRoomSystem.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(EscapeRoom))]
        public Guid EscapeRoomId { get; set; }
        public EscapeRoom? EscapeRoom { get; set; }

        [Required]
        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Range(2, 10)]
        public int GroupSize { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Confirmed";
    }
}