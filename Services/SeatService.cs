using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<IEnumerable<SeatResponse>> GetAllSeatsAsync()
        {
            var seats = await _seatRepository.GetAllAsync();
            return seats.Select(MapToResponse);
        }

        public async Task<SeatResponse?> GetSeatByIdAsync(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            return seat == null ? null : MapToResponse(seat);
        }

        public async Task<IEnumerable<SeatResponse>> GetSeatsByScreenIdAsync(int screenId)
        {
            var seats = await _seatRepository.GetByScreenIdAsync(screenId);
            return seats.Select(MapToResponse);
        }

        public async Task<SeatResponse> CreateSeatAsync(CreateSeatRequest request)
        {
            var seat = new Seat
            {
                ScreenId = request.ScreenId,
                RowName = request.RowName,
                SeatNumber = request.SeatNumber,
                SeatType = request.SeatType
            };

            var created = await _seatRepository.AddAsync(seat);
            return MapToResponse(created);
        }

        // Bulk Seats Create Method (Interface-kku thevaiyanathu)
        public async Task<bool> CreateBulkSeatsAsync(int screenId, List<CreateSeatRequest> seatRequests)
        {
            foreach (var request in seatRequests)
            {
                var seat = new Seat
                {
                    ScreenId = screenId,
                    RowName = request.RowName,
                    SeatNumber = request.SeatNumber,
                    SeatType = request.SeatType
                };
                await _seatRepository.AddAsync(seat);
            }
            return true;
        }

        public async Task<bool> UpdateSeatAsync(int id, CreateSeatRequest request)
        {
            var existing = await _seatRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.ScreenId = request.ScreenId;
            existing.RowName = request.RowName;
            existing.SeatNumber = request.SeatNumber;
            existing.SeatType = request.SeatType;

            return await _seatRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteSeatAsync(int id)
        {
            return await _seatRepository.DeleteAsync(id);
        }

        private static SeatResponse MapToResponse(Seat s) => new SeatResponse
        {
            SeatId = s.SeatId,
            ScreenId = s.ScreenId,
            ScreenName = s.Screen != null ? s.Screen.ScreenName : string.Empty,
            RowName = s.RowName,
            SeatNumber = s.SeatNumber,
            SeatType = s.SeatType
        };
    }
}