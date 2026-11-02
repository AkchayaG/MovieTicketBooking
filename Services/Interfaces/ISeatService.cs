using MovieTicketBooking.Dtos;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface ISeatService
    {
        Task<IEnumerable<SeatResponse>> GetAllSeatsAsync();
        Task<SeatResponse?> GetSeatByIdAsync(int id);
        Task<IEnumerable<SeatResponse>> GetSeatsByScreenIdAsync(int screenId);
        Task<SeatResponse> CreateSeatAsync(CreateSeatRequest request);
        Task<bool> CreateBulkSeatsAsync(int screenId, List<CreateSeatRequest> seatRequests); // <-- Bulk creation added
        Task<bool> UpdateSeatAsync(int id, CreateSeatRequest request);
        Task<bool> DeleteSeatAsync(int id);
    }
}