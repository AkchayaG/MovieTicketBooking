namespace MovieTicketBooking.Dtos
{
    public class ScreenResponse
    {
        public int ScreenId { get; set; }
        public string ScreenName { get; set; } = string.Empty;
        public int TheatreId { get; set; }
        public string TheatreName { get; set; } = string.Empty;
    }
}