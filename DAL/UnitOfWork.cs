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
