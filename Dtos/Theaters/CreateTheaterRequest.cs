namespace MovieTicketBooking.Dtos
{
    public class CreateTheaterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int TotalScreens { get; set; }
    }
}