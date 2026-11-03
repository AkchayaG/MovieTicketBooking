using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> GetAllAsync();
        Task<Show?> GetByIdAsync(int id);
        Task<IEnumerable<Show>> GetByMovieIdAsync(int movieId);
        Task<Show> AddAsync(Show show);
        Task<bool> UpdateAsync(Show show);
        Task<bool> DeleteAsync(int id);
    }
}