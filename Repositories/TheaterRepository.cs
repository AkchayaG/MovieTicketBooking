using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly ApplicationDbContext _context;

        public TheaterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Theater>> GetAllAsync()
        {
            return await _context.Theaters.ToListAsync();
        }

        public async Task<Theater?> GetByIdAsync(int id)
        {
            return await _context.Theaters.FindAsync(id);
        }

        public async Task<Theater> AddAsync(Theater theater)
        {
            await _context.Theaters.AddAsync(theater);
            await _context.SaveChangesAsync();
            return theater;
        }

        public async Task<bool> UpdateAsync(Theater theater)
        {
            _context.Theaters.Update(theater);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null) return false;

            _context.Theaters.Remove(theater);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}