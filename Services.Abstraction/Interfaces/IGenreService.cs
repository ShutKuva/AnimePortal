

using Core.DB;

namespace Services.Abstraction.Interfaces
{
    public interface IGenreService
    {
        Task<ICollection<Genre>> GetGenresAsync();
        Task<Genre> GetGenreAsync(int genreId);
        Task<Genre> GetGenreByNameAsync(string genreName);
        Task DeleteGenreAsync(int genreId);
    }
}
