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
        public async Task<IActionResult> AddVideoAsync([FromForm] IEnumerable<IFormFile> videos, string language, int animeId, [FromQuery] VideoTypes videoType)
        {
            var test = Request.Form;

            await _videoAdapter.AddVideosAsProducerOfLocalizationAsync(User, animeId, language, videoType, videos);

            return Ok();
        }

        [HttpGet("{language}/{animeId}/videos")]
        public async Task<OkObjectResult> GetVideos(string language, int animeId)
        {
            return Ok(await _videoAdapter.GetVideosOfAnime(animeId, User, language));
        }

        [HttpGet("videos/{id}")]
        public async Task<OkObjectResult> GetVideo(int id)
        {
            return Ok(await _videoAdapter.GetVideo(id));
        }

        [HttpDelete("videos/{id}")]
        public async Task<OkResult> DeleteVideo(int id)
        {
            await _videoAdapter.DeleteVideo(id);
            return Ok();
        }
    }
}
