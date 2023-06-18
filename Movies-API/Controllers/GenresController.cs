using Microsoft.AspNetCore.Mvc;


namespace Movies_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresServices _genresServices;

        public GenresController(IGenresServices genresServices)
        {
            _genresServices = genresServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresServices.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(byte id)
        {
            var genre = await _genresServices.GetById(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            Genre genre = new() { Name = dto.Name };
            await _genresServices.Add(genre);
            return Ok(genre);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(byte Id, [FromBody] GenreDto dto)
        {
            var genre = await _genresServices.GetById(Id);

            if (genre == null) { return NotFound($"No Genre Found With Id:{Id}"); }

            genre.Name = dto.Name;

            _genresServices.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(byte Id)
        {
            var genre = await _genresServices.GetById(Id);
            if (genre == null)
            {
                return NotFound($"No Genre Found With Id:{Id}");
            }
            _genresServices.Delete(genre);
            return Ok(genre);
        }
    }
}
