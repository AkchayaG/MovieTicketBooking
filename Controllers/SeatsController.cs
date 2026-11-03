
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        // 1. Get All Seats (Public)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _seatService.GetAllSeatsAsync());
        }

        // 2. Get Seat By ID (Public)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);

            if (seat == null)
                return NotFound();

            return Ok(seat);
        }

        // 3. Get Seats By Screen (Public)
        [HttpGet("screen/{screenId}")]
        public async Task<IActionResult> GetByScreen(int screenId)
        {
            return Ok(await _seatService.GetSeatsByScreenIdAsync(screenId));
        }

        // 4. Create Seat (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSeatRequest request)
        {
            var created = await _seatService.CreateSeatAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.SeatId },
                created);
        }

        // 5. Update Seat (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreateSeatRequest request)
        {
            var result = await _seatService.UpdateSeatAsync(id, request);

            if (!result)
                return NotFound();

            return Ok(new { message = "Seat updated successfully!" });
        }

        // 6. Delete Seat (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _seatService.DeleteSeatAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
