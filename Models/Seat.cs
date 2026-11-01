namespace MovieTicketBooking.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int ScreenId { get; set; } // Foreign Key
        public string RowName { get; set; } = string.Empty; // e.g., "A", "B"
        public int SeatNumber { get; set; } // e.g., 1, 2, 3
        public string SeatType { get; set; } = string.Empty; // e.g., "VIP", "Executive", "Regular"

        // Navigation Property back to Screen
        public Screen? Screen { get; set; }
    }
}