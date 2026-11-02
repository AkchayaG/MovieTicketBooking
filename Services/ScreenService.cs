using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenRepository _screenRepository;

        public ScreenService(IScreenRepository screenRepository)
        {
            _screenRepository = screenRepository;
        }

        public async Task<IEnumerable<ScreenResponse>> GetAllScreensAsync()
        {
            var screens = await _screenRepository.GetAllAsync();
            return screens.Select(MapToResponse);
        }

        public async Task<ScreenResponse?> GetScreenByIdAsync(int id)
        {
            var screen = await _screenRepository.GetByIdAsync(id);
            return screen == null ? null : MapToResponse(screen);
        }

        public async Task<IEnumerable<ScreenResponse>> GetScreensByTheatreIdAsync(int theatreId)
        {
            var screens = await _screenRepository.GetByTheatreIdAsync(theatreId);
            return screens.Select(MapToResponse);
        }

        public async Task<ScreenResponse> CreateScreenAsync(CreateScreenRequest request)
        {
            var screen = new Screen
            {
                ScreenName = request.ScreenName,
                TheaterId = request.TheaterId
            };

            var created = await _screenRepository.AddAsync(screen);
            return MapToResponse(created);
        }

        public async Task<bool> UpdateScreenAsync(int id, CreateScreenRequest request)
        {
            var existing = await _screenRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.ScreenName = request.ScreenName;
            existing.TheaterId = request.TheaterId;

            return await _screenRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteScreenAsync(int id)
        {
            return await _screenRepository.DeleteAsync(id);
        }

        private static ScreenResponse MapToResponse(Screen s) => new ScreenResponse
        {
            ScreenId = s.ScreenId,
            ScreenName = s.ScreenName,
            TheaterId = s.TheaterId,
            TheatreName = s.Theater != null ? s.Theater.Name : string.Empty
        };
    }
}