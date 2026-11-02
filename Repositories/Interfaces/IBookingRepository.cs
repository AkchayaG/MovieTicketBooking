using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task SaveChangesAsync();

        // Helper methods for validation and seat mapping
        Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds);
        Task<IEnumerable<Seat>> GetSeatsByScreenAndIdsAsync(int screenId, List<int> seatIds);
        Task<bool> IsAnySeatBookedAsync(int showId, List<int> seatIds);
        Task AddBookingSeatAsync(BookingSeat bookingSeat);
    }
}