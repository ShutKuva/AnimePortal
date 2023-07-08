using Core.DB;

namespace Services.Abstraction.Interfaces
{
    public interface ILocalizationService
    {
        Task<Localization> GetLocalizationAsync(Anime anime, Language language, User producer);

        Task<Localization> CreateLocalizationAsync(Anime anime, Language language, User producer);

        Task DeleteLocalizationAsync(int localizationId);
    }
}