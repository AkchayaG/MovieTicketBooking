using MovieTicketBooking.DTOs.Bookings;

namespace MovieTicketBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingResponse>> GetAllBookingsAsync();
        Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request);
        Task<bool> CancelBookingAsync(int id);
    }
}