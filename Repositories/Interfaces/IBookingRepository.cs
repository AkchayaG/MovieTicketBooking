using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetByShowIdAsync(int showId);
        Task<Booking> AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
   
        Task SaveChangesAsync();
        Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds);
    }
}