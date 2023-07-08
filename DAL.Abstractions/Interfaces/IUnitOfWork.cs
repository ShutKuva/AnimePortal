
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
        IVideoRepository VideoRepository { get; }
        ILocalizationRepository LocalizationRepository { get; }
        ICloudinaryVideoRepository CloudinaryVideoRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
