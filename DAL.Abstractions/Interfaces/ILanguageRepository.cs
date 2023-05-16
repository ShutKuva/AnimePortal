using Core.DB;

namespace DAL.Abstractions.Interfaces
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task<ICollection<Language>> GetAllLanguagesAsync();
    }
}
