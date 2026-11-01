namespace MovieTicketBooking.Models
{
    public class Screen
    {
        public int ScreenId { get; set; }
        public string ScreenName { get; set; } = string.Empty;

        // Foreign Key
        public int TheatreId { get; set; }

        // Navigation Property back to Theatre
        public Theater? Theater { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<Show> Shows { get; set; } = new List<Show>();
    }
}