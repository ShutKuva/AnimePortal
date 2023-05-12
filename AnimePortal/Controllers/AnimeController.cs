using BLL.Abstractions.Interfaces.Adapters;
using Core.DB;
using Core.DTOs.Anime;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;

namespace AnimePortalAuthServer.Controllers
{
    public class AnimeController : BaseController
    {
        private readonly IAnimeService _animeService;
        private readonly IAnimePreviewAdapter _animePreviewAdapter;

        public AnimeController(IAnimeService animeService, IAnimePreviewAdapter animePreviewAdapter)
        {
            _animeService = animeService;
            _animePreviewAdapter = animePreviewAdapter;
        }

        [HttpGet("{animeId}")]
        public async Task<ActionResult<Anime>> GetAnimeAsync(int animeId)
        {
            Anime anime = await _animeService.GetAnimeAsync(animeId);
            return Ok(anime);
        }

        [HttpGet("{language}/preview/{animeId}")]
        public async Task<ActionResult<AnimePreview>> GetAnimePreviewAsync(int animeId, string language)
        {
            AnimePreview animePreview = await _animePreviewAdapter.GetAnimePreviewAsync(animeId, language);

            return Ok(animePreview);
        }

        [HttpGet("{language}/previews/{quantity}")]
        public async Task<ActionResult<ICollection<AnimePreview>>> GetAnimePreviewsAsync(int quantity, string language)
        {
            var animePreivews = await _animePreviewAdapter.GetAnimePreviewsAsync(quantity, language);

            return Ok(animePreivews);
        }

        [HttpPost("create")]
        public async Task<CreatedResult> CreateAnimeAsync([FromBody] AnimeDto animeDto)
        {
            await _animeService.CreateAsync(animeDto);

            return Created(string.Empty, animeDto);
        }

        [HttpPost("update/{animeId}")]
        public async Task<ActionResult<AnimeDto>> UpdateAnimeAsync([FromBody] AnimeDto animeDto, int animeId)
        {
            await _animeService.UpdateAnimeAsync(animeDto, animeId);

            return Ok(animeDto);
        }

        [HttpPost("add/photo/{animeId}/{photoType}")]
        public async Task<ActionResult<Photo>> AddAnimePhoto(IFormFile file, int animeId, PhotoTypes photoType)
        {
            var photo = await _animeService.AddAnimePhotoAsync(file, animeId, photoType);

            return Ok(photo);
        }

        [HttpDelete("delete/anime/{animeId}")]
        public async Task<IActionResult> DeleteAnimeAsync(int animeId)
        {
            await _animeService.DeleteAnimeAsync(animeId);

            return Ok("Successfully!");
        }

        [HttpDelete("delete/photo/{animeId}/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int animeId, int photoId)
        {
            await _animeService.DeleteAnimePhotoAsync(animeId, photoId);

            return Ok("Successfully!");
        }
    }
}
