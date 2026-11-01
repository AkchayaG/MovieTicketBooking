using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly ApplicationDbContext _context;

        public ScreenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Screen>> GetAllAsync()
        {
            return await _context.Screens.Include(s => s.Theater).ToListAsync();
        }

        public async Task<Screen?> GetByIdAsync(int id)
        {
            return await _context.Screens.Include(s => s.Theater).FirstOrDefaultAsync(s => s.ScreenId == id);
        }

        public async Task<IEnumerable<Screen>> GetByTheatreIdAsync(int theatreId)
        {
            return await _context.Screens
                .Where(s => s.TheatreId == theatreId)
                .Include(s => s.Theater)
                .ToListAsync();
        }

        public async Task<Screen> AddAsync(Screen screen)
        {
            await _context.Screens.AddAsync(screen);
            await _context.SaveChangesAsync();
            return screen;
        }

        public async Task<bool> UpdateAsync(Screen screen)
        {
            _context.Screens.Update(screen);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var screen = await _context.Screens.FindAsync(id);
            if (screen == null) return false;

            _context.Screens.Remove(screen);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}