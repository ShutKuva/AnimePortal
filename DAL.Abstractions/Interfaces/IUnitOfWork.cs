
namespace DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IAnimeRepository AnimeRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IGenreRepository GenreRepository { get; }
        ICommentRepository CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
