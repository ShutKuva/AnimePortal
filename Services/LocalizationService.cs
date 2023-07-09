using Core.DB;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWork _unitOfWork;

        public LocalizationService(ILanguageService languageService, IUnitOfWork unitOfWork)
        {
            _languageService = languageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Localization> GetLocalizationAsync(Anime anime, Language language, User producer)
        {
            IEnumerable<Localization> localizations = await _unitOfWork.LocalizationRepository.ReadByConditionAsync(l => l.Language == language && l.LocalizationProducerId == producer.Id && l.Anime == anime);

            if (localizations.Any())
            {
                return localizations.First();
            }

            return await CreateLocalizationAsync(anime, language, producer);
        }

        public async Task<Localization> CreateLocalizationAsync(Anime anime, Language language, User producer)
        {
            Language? actualLanguage = (await _languageService.GetLanguageByNameAsync(language.Name)) ?? language;

            Localization newLocalization = new Localization()
            {
                Language = actualLanguage,
                LocalizationProducerId = producer.Id,
                AnimeId = anime.Id,
            };

            await _unitOfWork.LocalizationRepository.CreateAsync(newLocalization);

            await _unitOfWork.SaveChangesAsync();

            return newLocalization;
        }

        public async Task DeleteLocalizationAsync(int localizationId)
        {
            await _unitOfWork.LocalizationRepository.DeleteAsync(localizationId);
        }
    }
}