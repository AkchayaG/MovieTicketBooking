using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _seatService.GetAllSeatsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);
            if (seat == null) return NotFound();
            return Ok(seat);
        }

        [HttpGet("screen/{screenId}")]
        public async Task<IActionResult> GetByScreen(int screenId)
        {
            return Ok(await _seatService.GetSeatsByScreenIdAsync(screenId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSeatRequest request)
        {
            var created = await _seatService.CreateSeatAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.SeatId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSeatRequest request)
        {
            var result = await _seatService.UpdateSeatAsync(id, request);
            if (!result) return NotFound();
            return Ok(new { message = "Seat updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _seatService.DeleteSeatAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}