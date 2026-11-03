using MovieTicketBooking.Dtos;

namespace MovieTicketBooking.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<ShowResponse>> GetAllShowsAsync();
        Task<ShowResponse?> GetShowByIdAsync(int id);
        Task<IEnumerable<ShowResponse>> GetShowsByMovieIdAsync(int movieId);
        Task<ShowResponse> CreateShowAsync(CreateShowRequest request);
        Task<bool> UpdateShowAsync(int id, CreateShowRequest request);
        Task<bool> DeleteShowAsync(int id);
    }
}