using Core.DB;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _uow;

        public LanguageService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<Language>> GetLanguagesAsync()
        {
            var languages = await _uow.LanguageRepository.GetAllLanguagesAsync();
            return languages;
        }

        public async Task<Language> GetLanguageAsync(int languageId)
        {
            var language = await _uow.LanguageRepository.ReadAsync(languageId);
            return language!;
        }

        public async Task<Language> GetLanguageByNameAsync(string languageName)
        {
            var language = await _uow.LanguageRepository.ReadByConditionAsync(lang => lang.Name == languageName);
            return language.FirstOrDefault()!;
        }

        public async Task DeleteLanguageAsync(int languageId)
        {
            await _uow.LanguageRepository.DeleteAsync(languageId);
            await _uow.SaveChangesAsync();
        }

    }
}
