using MovieTicketBooking.DTOs.Bookings;

namespace MovieTicketBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse?> GetBookingByIdAsync(
            int id, int userId, bool isAdmin);

        Task<IEnumerable<BookingResponse>> GetAllBookingsAsync();

        Task<IEnumerable<BookingResponse>> GetMyBookingsAsync(int userId);

        Task<BookingResponse> CreateBookingAsync(
            CreateBookingRequest request, int userId);

        Task<bool> CancelBookingAsync(
            int id, int userId, bool isAdmin);
    }
}