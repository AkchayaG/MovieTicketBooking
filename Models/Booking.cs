using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBooking.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [MaxLength(30)]
        public string BookingReference { get; set; } = string.Empty;

        public int ShowId { get; set; }
        [ForeignKey("ShowId")]
        public Show? Show { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string CustomerPhone { get; set; } = string.Empty; // Note: 'get { set; }' check pannikonga

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Confirmed";
        public ICollection<BookingSeat>? BookingSeats { get; set; }// Confirmed, Cancelled
    }
}