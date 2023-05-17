using Core.DB;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _uow;

        public GenreService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            var genres = await _uow.GenreRepository.GetAllGenressAsync();
            return genres;
        }

        public async Task<Genre> GetGenreAsync(int genreId)
        {
            var genre = await _uow.GenreRepository.ReadAsync(genreId);
            return genre!;
        }

        public async Task<Genre> GetGenreByNameAsync(string genreName)
        {
            var genre = await _uow.GenreRepository.ReadByConditionAsync(genr => genr.Name == genreName);
            return genre.FirstOrDefault()!;
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            await _uow.GenreRepository.DeleteAsync(genreId);
            await _uow.SaveChangesAsync();
        }
    }
}
