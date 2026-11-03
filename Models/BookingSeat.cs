using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBooking.Models
{
    public class BookingSeat
    {
        [Key]
        public int BookingSeatId { get; set; }

        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        public int SeatId { get; set; }
        [ForeignKey("SeatId")]
        public Seat? Seat { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}