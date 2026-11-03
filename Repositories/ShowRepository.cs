using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly ApplicationDbContext _context;

        public ShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Show>> GetAllAsync()
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .ToListAsync();
        }

        public async Task<Show?> GetByIdAsync(int id)
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .FirstOrDefaultAsync(s => s.ShowId == id);
        }

        public async Task<IEnumerable<Show>> GetByMovieIdAsync(int movieId)
        {
            return await _context.Shows
                .Where(s => s.MovieId == movieId)
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .ToListAsync();
        }

        public async Task<Show> AddAsync(Show show)
        {
            try
            {
                await _context.Shows.AddAsync(show);
                await _context.SaveChangesAsync();
                return show;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                // Inner exception message-a print panrom
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Database update error: {innerExceptionMessage}");
            }
        }
        public async Task<bool> UpdateAsync(Show show)
        {
            _context.Shows.Update(show);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show == null) return false;

            _context.Shows.Remove(show);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
