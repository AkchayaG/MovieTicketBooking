namespace MovieTicketBooking.Models
{
    public class Show
    {
        public int ShowId { get; set; }

        // Foreign Keys
        public int MovieId { get; set; }
        public int ScreenId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
        public decimal TicketPrice { get; set; }

        // Navigation Properties
        public Movie? Movie { get; set; }
        public Screen? Screen { get; set; }

    }
}