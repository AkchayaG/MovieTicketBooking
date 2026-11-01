using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class TheaterService : ITheaterService
    {
        private readonly ITheaterRepository _theaterRepository;

        public TheaterService(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<IEnumerable<TheaterResponse>> GetAllTheatersAsync()
        {
            var theaters = await _theaterRepository.GetAllAsync();
            return theaters.Select(t => MapToResponse(t));
        }

        public async Task<TheaterResponse?> GetTheaterByIdAsync(int id)
        {
            var theater = await _theaterRepository.GetByIdAsync(id);
            return theater == null ? null : MapToResponse(theater);
        }

        public async Task<TheaterResponse> CreateTheaterAsync(CreateTheaterRequest request)
        {
            var theater = new Theater
            {
                Name = request.Name,
                Location = request.Location,
                City = request.City,
                TotalScreens = request.TotalScreens
            };

            var created = await _theaterRepository.AddAsync(theater);
            return MapToResponse(created);
        }

        public async Task<bool> UpdateTheaterAsync(int id, CreateTheaterRequest request)
        {
            var existing = await _theaterRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = request.Name;
            existing.Location = request.Location;
            existing.City = request.City;
            existing.TotalScreens = request.TotalScreens;

            return await _theaterRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteTheaterAsync(int id)
        {
            return await _theaterRepository.DeleteAsync(id);
        }

        private static TheaterResponse MapToResponse(Theater t) => new TheaterResponse
        {
            TheaterId = t.TheaterId,
            Name = t.Name,
            Location = t.Location,
            City = t.City,
            TotalScreens = t.TotalScreens
        };
    }
}