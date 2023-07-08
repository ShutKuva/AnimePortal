using DAL.Abstractions.Interfaces;
using DAL.Repositories;
using DAL.Repositories.VideoRepositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthServerContext _context;

        public UnitOfWork(AuthServerContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository => new UserRepository(_context);
        public IAnimeRepository AnimeRepository => new AnimeRepository(_context);
        public IPhotoRepository PhotoRepository => new PhotoRepository(_context);
        public ILanguageRepository LanguageRepository => new LanguageRepository(_context);
        public IRelatedAnimeRepository RelatedAnimeRepository => new RelatedAnimeRepository(_context);
        public IGenreRepository GenreRepository => new GenreRepository(_context);
        public ICommentRepository CommentRepository => new CommentRepository(_context);
        public IVideoRepository VideoRepository => new VideoRepository(_context);
        public ILocalizationRepository LocalizationRepository => new LocalizationRepository(_context);
        public ICloudinaryVideoRepository CloudinaryVideoRepository => new CloudinaryVideoRepository(_context);
        public IPlayerRepository PlayerRepository => new PlayerRepository(_context);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
