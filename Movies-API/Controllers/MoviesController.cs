using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies_API.Services;

namespace Movies_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenresServices _genresService;
        private readonly IMapper _mapper;
        public MoviesController(IMovieService movieService, IGenresServices genresService, IMapper mapper)
        {
            _movieService = movieService;
            _genresService = genresService;
            _mapper = mapper;
        }

        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _allowedFileSize = 1048576;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Movies = await _movieService.GetAll();

            var data = _mapper.Map<IEnumerable<MovieDto>>(Movies);
            return Ok(data);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = _movieService.GetById(id);

            if (movie == null)
                return NotFound();
            var dto = _mapper.Map<MovieDto>(movie);
            return Ok(dto);
        }


        [HttpGet("GetByGenreAsync")]
        public async Task<IActionResult> GetByGenreAsync(byte genreid)
        {
            var movie = await _movieService.GetAll(genreid);
            if (movie == null)
            { return BadRequest($"There`s Movies With This Genre"); }
            var dto = _mapper.Map<IEnumerable<MovieDto>>(movie);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieCreate dto)
        {
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("The Allowed Extenstions is (.jpg) And (.png) !");
            if (dto.Poster.Length > _allowedFileSize)
                return BadRequest("The Allowed File Size Is 1MG !");

            var ValidGenreId = await _genresService.ValidGenreId(dto.GenreId);

            if (!ValidGenreId == null)
                return BadRequest("The GenreID is Invalid");

            var dataStream = new MemoryStream();


            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();


            _movieService.Create(movie);
            return Ok(movie);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieUpdate movie)
        {
            var Movie = await _movieService.GetById(id);
            if (Movie == null) return BadRequest($"There`s Movie With This ID:{id}");

            var ValidGenreId = await _genresService.ValidGenreId(movie.GenreId);
            if (!ValidGenreId)
                return BadRequest("The GenreID is Invalid");

            if (movie.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(movie.Poster.FileName).ToLower()))
                    return BadRequest("The Allowed Extenstions is (.jpg) And (.png) !");

                if (_allowedFileSize < movie.Poster.Length)
                    return BadRequest("The Allowed File Size Is 1MG !");

                var dataStream = new MemoryStream();
                await movie.Poster.CopyToAsync(dataStream);

                Movie.Poster = dataStream.ToArray();
            }
            Movie.Title = movie.Title;
            Movie.Year = movie.Year;
            Movie.StoryLine = movie.StoryLine;
            Movie.Rate = movie.Rate;
            Movie.GenreId = movie.GenreId;

            _movieService.Update(Movie);

            return Ok(Movie);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _movieService.GetById(id);

            if (movie == null) return BadRequest($"There`s No Movie With This ID : {id}");

            _movieService.Delete(movie);
            return Ok(movie);

        }
    }
}
