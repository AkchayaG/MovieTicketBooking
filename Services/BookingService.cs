using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IShowRepository _showRepository;

        public BookingService(IBookingRepository bookingRepository, IShowRepository showRepository)
        {
            _bookingRepository = bookingRepository;
            _showRepository = showRepository;
        }

        public async Task<BookingResponse?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return null;

            // Fetch booked seats for this booking
            var bookingSeats = await _bookingRepository.GetShowSeatsAsync(booking.ShowId, new List<int>()); // or query BookingSeats table directly via context if needed
            // Let's map directly:
            return MapToResponse(booking);
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return bookings.Select(MapToResponse);
        }

        public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request)
        {
            var show = await _showRepository.GetByIdAsync(request.ShowId);
            if (show == null) throw new KeyNotFoundException("Show not found.");

            var showSeats = await _bookingRepository.GetShowSeatsAsync(request.ShowId, request.SeatIds);

            if (showSeats.Count != request.SeatIds.Count)
                throw new InvalidOperationException("One or more requested seats are invalid for this show.");

            foreach (var ss in showSeats)
            {
                if (ss.Status != "Available")
                    throw new InvalidOperationException($"Seat {ss.Seat?.RowName}{ss.Seat?.SeatNumber} is already booked.");
            }

            decimal totalAmount = show.TicketPrice * request.SeatIds.Count;

            var booking = new Booking
            {
                BookingReference = "MOV" + new Random().Next(1000, 9999),
                ShowId = request.ShowId,
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone,
                BookingDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Confirmed"
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync(); // To get BookingId

            // Add BookingSeats and update ShowSeats status
            foreach (var ss in showSeats)
            {
                ss.Status = "Booked";
                // If you have DbContext directly or via repository, add BookingSeats
            }

            await _bookingRepository.SaveChangesAsync();

            return MapToResponse(booking);
        }

        public async Task<bool> CancelBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null || booking.Status == "Cancelled") return false;

            booking.Status = "Cancelled";
            await _bookingRepository.UpdateAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        private BookingResponse MapToResponse(Booking booking)
        {
            return new BookingResponse
            {
                BookingId = booking.BookingId,
                BookingReference = booking.BookingReference,
                ShowId = booking.ShowId,
                MovieTitle = booking.Show?.Movie?.Title ?? string.Empty,
                TheatreName = booking.Show?.Screen?.Theater?.Name ?? string.Empty,
                ShowStartTime = booking.Show?.StartTime ?? default,
                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                CustomerPhone = booking.CustomerPhone,
                BookingDate = booking.BookingDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
                Seats = new List<string>() // Can be populated from BookingSeats
            };
        }
    }
}