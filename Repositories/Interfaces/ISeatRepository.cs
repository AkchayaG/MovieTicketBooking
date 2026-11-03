using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetAllAsync();
        Task<Seat?> GetByIdAsync(int id);
        Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId);
        Task<Seat> AddAsync(Seat seat);
        Task<bool> UpdateAsync(Seat seat);
        Task<bool> DeleteAsync(int id);
    }
}