using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.DTOs.Movies;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Select(m => new MovieResponse
                {
                    MovieId = m.MovieId, // Corrected to MovieId
                    Title = m.Title,
                    Description = m.Description,
                    Genre = m.Genre,
                    Language = m.Language,
                    DurationMinutes = m.DurationMinutes,
                    ReleaseDate = m.ReleaseDate
                })
                .ToListAsync();
        }

        public async Task<MovieResponse?> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return null;

            return new MovieResponse
            {
                MovieId = movie.MovieId, // Corrected to MovieId
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Language = movie.Language,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate
            };
        }

        public async Task<IEnumerable<MovieResponse>> SearchMoviesByTitleAsync(string title)
        {
            return await _context.Movies
                .Where(m => m.Title.Contains(title))
                .Select(m => new MovieResponse
                {
                    MovieId = m.MovieId, // Corrected to MovieId
                    Title = m.Title,
                    Description = m.Description,
                    Genre = m.Genre,
                    Language = m.Language,
                    DurationMinutes = m.DurationMinutes,
                    ReleaseDate = m.ReleaseDate
                })
                .ToListAsync();
        }

        public async Task<MovieResponse> CreateMovieAsync(CreateMovieRequest request)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
                Genre = request.Genre,
                Language = request.Language,
                DurationMinutes = request.DurationMinutes,
                ReleaseDate = request.ReleaseDate
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return new MovieResponse
            {
                MovieId = movie.MovieId, // Corrected to MovieId
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Language = movie.Language,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate
            };
        }

        public async Task<bool> UpdateMovieAsync(int id, UpdateMovieRequest request)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.Genre = request.Genre;
            movie.Language = request.Language;
            movie.DurationMinutes = request.DurationMinutes;
            movie.ReleaseDate = request.ReleaseDate;

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}