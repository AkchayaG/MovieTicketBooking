using MovieTicketBooking.Dtos;

namespace MovieTicketBooking.Interfaces
{
    public interface IScreenService
    {
        Task<IEnumerable<ScreenResponse>> GetAllScreensAsync();
        Task<ScreenResponse?> GetScreenByIdAsync(int id);
        Task<IEnumerable<ScreenResponse>> GetScreensByTheatreIdAsync(int theatreId);
        Task<ScreenResponse> CreateScreenAsync(CreateScreenRequest request);
        Task<bool> UpdateScreenAsync(int id, CreateScreenRequest request);
        Task<bool> DeleteScreenAsync(int id);
    }
}
