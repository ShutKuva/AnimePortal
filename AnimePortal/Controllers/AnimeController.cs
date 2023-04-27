using AutoMapper;
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
        private readonly IMapper _mapper;

        public AnimeController(IAnimeService animeService, IMapper mapper)
        {
            _animeService = animeService;
            _mapper = mapper;
        }

        [HttpGet("preview/{animeId}")]
        public async Task<ActionResult<AnimePreview>> GetAnimePreviewAsync(int animeId)
        {
            AnimePreview animePreview = await _animeService.GetAnimePreviewAsync(animeId);
            return Ok(animePreview);
        }

        [HttpGet("previews/{quantity}")]
        public async Task<ActionResult<ICollection<AnimePreview>>> GetAnimePreviewsAsync(int quantity)
        {
            var animes = await _animeService.GetAnimePreviewsAsync(quantity);

            return Ok(animes);
        }

        [HttpPost("create")]
        public async Task<CreatedResult> CreateAnimeAsync([FromBody] AnimeDto animeDto)
        {
            var anime = _mapper.Map<Anime>(animeDto);
            anime.Date = DateTime.SpecifyKind(anime.Date, DateTimeKind.Utc);

            await _animeService.CreateAsync(anime);
            return Created(string.Empty, anime);
        }

        [HttpPost("update/{animeId}")]
        public async Task<ActionResult<AnimePreview>> UpdateAnimeAsync([FromBody] AnimeDto animeDto, int animeId)
        {
            Anime anime = _mapper.Map<Anime>(animeDto);
            anime.Id = animeId;
            anime.Date = DateTime.SpecifyKind(anime.Date, DateTimeKind.Utc);

            await _animeService.UpdateAnimeAsync(anime);
            AnimePreview animePreview = _mapper.Map<AnimePreview>(await _animeService.GetAnimeAsync(animeId));

            return Ok(animePreview);
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
