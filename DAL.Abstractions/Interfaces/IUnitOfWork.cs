using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstractions.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IAnimeRepository AnimeRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IGenreRepository GenreRepository { get; }
        IMailTemplateRepository MailTemplateRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
