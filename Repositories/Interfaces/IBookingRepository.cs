using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(int id);

        Task<IEnumerable<Booking>> GetAllAsync();

        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);

        Task AddAsync(Booking booking);

        Task UpdateAsync(Booking booking);

        Task SaveChangesAsync();

        Task<List<ShowSeat>> GetShowSeatsAsync(
            int showId, List<int> seatIds);

        Task<IEnumerable<Seat>> GetSeatsByScreenAndIdsAsync(
            int screenId, List<int> seatIds);

        Task<bool> IsAnySeatBookedAsync(
            int showId, List<int> seatIds);

        Task AddBookingSeatAsync(BookingSeat bookingSeat);
    }
}