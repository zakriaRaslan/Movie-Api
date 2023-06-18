namespace Movies_API.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _context.Movies
                  .Where(x => x.GenreId == genreId || genreId == 0)
                  .Include(x => x.Genre)
                  .OrderByDescending(x => x.Rate)
                  .ToListAsync();
        }

        public async Task<Movie> GetById(int Id)
        {
            return await _context.Movies.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();

            return movie;
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }


    }
}
