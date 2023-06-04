using DAL.Abstractions.Interfaces;
using DAL.Repositories;

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
