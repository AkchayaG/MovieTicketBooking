using MovieTicketBooking.Dtos;

namespace MovieTicketBooking.Interfaces
{
    public interface ITheaterService
    {
        Task<IEnumerable<TheaterResponse>> GetAllTheatersAsync();
        Task<TheaterResponse?> GetTheaterByIdAsync(int id);
        Task<TheaterResponse> CreateTheaterAsync(CreateTheaterRequest request);
        Task<bool> UpdateTheaterAsync(int id, CreateTheaterRequest request);
        Task<bool> DeleteTheaterAsync(int id);
    }
}