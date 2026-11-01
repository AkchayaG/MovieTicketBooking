using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface ITheaterRepository
    {
        Task<IEnumerable<Theater>> GetAllAsync();
        Task<Theater?> GetByIdAsync(int id);
        Task<Theater> AddAsync(Theater theater);
        Task<bool> UpdateAsync(Theater theater);
        Task<bool> DeleteAsync(int id);
    }
}