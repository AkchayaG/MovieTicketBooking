using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Movies;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // 1. Get All Movies (Public)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAll()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        // 2. Get Movie By ID (Public)
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieResponse>> GetById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        // 3. Search Movies by Title (Public)
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> Search([FromQuery] string title)
        {
            var movies = await _movieService.SearchMoviesByTitleAsync(title);
            return Ok(movies);
        }

        // 4. Create Movie (Admin Only) -> Using CreateMovieAsync to match Service
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MovieResponse>> Create([FromBody] CreateMovieRequest request)
        {
            var movie = await _movieService.CreateMovieAsync(request);
            // Ungal model-il primary key 'MovieId' irunthal inge movie.MovieId enru use seiyavum
            return CreatedAtAction(nameof(GetById), new { id = movie.MovieId }, movie);
        }

        // 5. Update Movie (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieRequest request)
        {
            var result = await _movieService.UpdateMovieAsync(id, request);
            if (!result) return NotFound();
            return Ok(new { message = "Movie updated successfully!" });
        }

        // 6. Delete Movie (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _movieService.DeleteMovieAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}