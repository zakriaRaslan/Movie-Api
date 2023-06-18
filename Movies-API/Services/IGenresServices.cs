

public interface IGenresServices
{
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre> GetById(byte id);
    Task<Genre> Add(Genre genre);

    Genre Delete(Genre genre);
    Genre Update(Genre genre);
    Task<bool> ValidGenreId(byte id);


}

