namespace MovieTicketBooking.Dtos
{
    public class CreateScreenRequest
    {
        public string ScreenName { get; set; } = string.Empty;
        public int TheatreId { get; set; }
    }
}
