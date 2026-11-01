using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;

        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public async Task<IEnumerable<ShowResponse>> GetAllShowsAsync()
        {
            var shows = await _showRepository.GetAllAsync();
            return shows.Select(MapToResponse);
        }

        public async Task<ShowResponse?> GetShowByIdAsync(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);
            return show == null ? null : MapToResponse(show);
        }

        public async Task<IEnumerable<ShowResponse>> GetShowsByMovieIdAsync(int movieId)
        {
            var shows = await _showRepository.GetByMovieIdAsync(movieId);
            return shows.Select(MapToResponse);
        }

        public async Task<ShowResponse> CreateShowAsync(CreateShowRequest request)
        {
            var show = new Show
            {
                MovieId = request.MovieId,
                ScreenId = request.ScreenId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Price = request.Price
            };

            var created = await _showRepository.AddAsync(show);
            return MapToResponse(created);
        }

        public async Task<bool> UpdateShowAsync(int id, CreateShowRequest request)
        {
            var existing = await _showRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.MovieId = request.MovieId;
            existing.ScreenId = request.ScreenId;
            existing.StartTime = request.StartTime;
            existing.EndTime = request.EndTime;
            existing.Price = request.Price;

            return await _showRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteShowAsync(int id)
        {
            return await _showRepository.DeleteAsync(id);
        }

        private static ShowResponse MapToResponse(Show s) => new ShowResponse
        {
            ShowId = s.ShowId,
            MovieId = s.MovieId,
            MovieTitle = s.Movie != null ? s.Movie.Title : string.Empty,
            ScreenId = s.ScreenId,
            ScreenName = s.Screen != null ? s.Screen.ScreenName : string.Empty,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Price = s.Price
        };
    }
}