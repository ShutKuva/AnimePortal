using Core.DB;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;

namespace AnimePortalAuthServer.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<Language>>> GetAllLanguage()
        {
            var languages = await _languageService.GetLanguagesAsync();
            return Ok(languages);
        }

    }
}
