using Adapters.Abstractions;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimePortalAuthServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : BaseController
    {
        private readonly IVideoAdapter _videoAdapter;

        public VideoController(IVideoAdapter videoAdapter)
        {
            _videoAdapter = videoAdapter;
        }

        [HttpPost("{language}/{animeId}/videos")]
        public async Task<IActionResult> AddVideoAsync(string language, int animeId, [FromQuery] VideoTypes videoType, IFormFile[] files)
        {
            await _videoAdapter.AddVideosAsProducerOfLocalizationAsync(User, animeId, language, videoType, files);

            return Ok();
        }
    }
}
