using MovieTicketBooking.Dtos;

namespace MovieTicketBooking.Interfaces
{
    public interface ISeatService
    {
        Task<IEnumerable<SeatResponse>> GetAllSeatsAsync();
        Task<SeatResponse?> GetSeatByIdAsync(int id);
        Task<IEnumerable<SeatResponse>> GetSeatsByScreenIdAsync(int screenId);
        Task<SeatResponse> CreateSeatAsync(CreateSeatRequest request);
        Task<bool> UpdateSeatAsync(int id, CreateSeatRequest request);
        Task<bool> DeleteSeatAsync(int id);
    }
}
