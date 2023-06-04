
namespace DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IAnimeRepository AnimeRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IGenreRepository GenreRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IRelatedAnimeRepository RelatedAnimeRepository { get; }
        ICommentRepository CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
