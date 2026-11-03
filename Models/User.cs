namespace MovieTicketBooking.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Customer";

        // One user can have multiple bookings
        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();
    }
}