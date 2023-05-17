
using Core.DB;

namespace Services.Abstraction.Interfaces
{
    public interface ILanguageService
    {
        Task<ICollection<Language>> GetLanguagesAsync();
        Task<Language> GetLanguageAsync(int languageId);
        Task<Language> GetLanguageByNameAsync(string languageName);
        Task DeleteLanguageAsync(int languageId);
        
    }
}
