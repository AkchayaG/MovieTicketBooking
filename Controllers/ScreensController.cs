using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService _screenService;

        public ScreensController(IScreenService screenService)
        {
            _screenService = screenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _screenService.GetAllScreensAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var screen = await _screenService.GetScreenByIdAsync(id);
            if (screen == null) return NotFound();
            return Ok(screen);
        }

        [HttpGet("theatre/{theatreId}")]
        public async Task<IActionResult> GetByTheatre(int theatreId)
        {
            return Ok(await _screenService.GetScreensByTheatreIdAsync(theatreId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScreenRequest request)
        {
            var created = await _screenService.CreateScreenAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.ScreenId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateScreenRequest request)
        {
            var result = await _screenService.UpdateScreenAsync(id, request);
            if (!result) return NotFound();
            return Ok(new { message = "Screen updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _screenService.DeleteScreenAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}