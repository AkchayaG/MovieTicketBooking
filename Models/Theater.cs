namespace MovieTicketBooking.Models
{
    public class Theater
    {
        public int TheaterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int TotalScreens { get; set; }
        public ICollection<Screen> Screens { get; set; } = new List<Screen>();
    }
}