using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheatersController : ControllerBase
    {
        private readonly ITheaterService _theaterService;

        public TheatersController(ITheaterService theaterService)
        {
            _theaterService = theaterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var theaters = await _theaterService.GetAllTheatersAsync();
            return Ok(theaters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var theater = await _theaterService.GetTheaterByIdAsync(id);
            if (theater == null) return NotFound();
            return Ok(theater);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTheaterRequest request)
        {
            var created = await _theaterService.CreateTheaterAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.TheaterId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTheaterRequest request)
        {
            var result = await _theaterService.UpdateTheaterAsync(id, request);
            if (!result) return NotFound();
            return Ok(new { message = "Theater updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _theaterService.DeleteTheaterAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}