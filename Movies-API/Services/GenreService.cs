
public class GenreService : IGenresServices
{
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Genre> Add(Genre genre)
    {
        await _context.AddAsync(genre);
        _context.SaveChanges();
        return genre;
    }

    public Genre Delete(Genre genre)
    {
        _context.Remove(genre);
        _context.SaveChanges();
        return genre;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _context.Genres.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Genre> GetById(byte id)
    {
        return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
    }

    public Genre Update(Genre genre)
    {
        _context.Update(genre);
        _context.SaveChanges();
        return genre;
    }



    public async Task<bool> ValidGenreId(byte id)
    {
        return await _context.Genres.AnyAsync(g => g.Id == id);

    }
}

