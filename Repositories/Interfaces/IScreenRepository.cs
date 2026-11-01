using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface IScreenRepository
    {
        Task<IEnumerable<Screen>> GetAllAsync();
        Task<Screen?> GetByIdAsync(int id);
        Task<IEnumerable<Screen>> GetByTheatreIdAsync(int theatreId);
        Task<Screen> AddAsync(Screen screen);
        Task<bool> UpdateAsync(Screen screen);
        Task<bool> DeleteAsync(int id);
    }
}