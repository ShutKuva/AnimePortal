using Adapters.Abstractions;
using Core.DB;
using Core.DTOs.Anime;
using Core.DTOs.Others;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;

namespace AnimePortalAuthServer.Controllers
{
    public class AnimeController : BaseController
    {
        private readonly IAnimeService _animeService;
        private readonly IAnimePreviewAdapter _animePreviewAdapter;
        private readonly IAnimeDetailedAdapter _animeDetailedAdapter;

        public AnimeController(IAnimeService animeService, IAnimePreviewAdapter animePreviewAdapter, IAnimeDetailedAdapter animeDetailedAdapter)
        {
            _animeService = animeService;
            _animePreviewAdapter = animePreviewAdapter;
            _animeDetailedAdapter = animeDetailedAdapter;
        }


        [HttpGet("{animeId}")]
        public async Task<ActionResult<Anime>> GetAnimeAsync(int animeId)
        {
            Anime anime = await _animeService.GetAnimeAsync(animeId);
            return Ok(anime);
        }

        [HttpGet("{language}/previews/{animeId}")]
        public async Task<ActionResult<AnimePreview>> GetAnimePreviewAsync(int animeId, string language)
        {
            AnimePreview animePreview = await _animePreviewAdapter.GetAnimePreviewAsync(animeId, language);

            return Ok(animePreview);
        }

        [HttpGet("{language}/previews")]
        public async Task<ActionResult<ICollection<AnimePreview>>> GetAnimePreviewsAsync(string language)
        {
            var animePreivews = await _animePreviewAdapter.GetAnimePreviewsAsync(10, language);

            return Ok(animePreivews);
        }

        [HttpGet("{language}/detailed/{animeId}")]
        public async Task<ActionResult<AnimePreview>> GetAnimeDetailAsync(int animeId, string language)
        {
            AnimeDetailed animeDetailed = await _animeDetailedAdapter.GetAnimeDetailedAsync(animeId, language);

            return Ok(animeDetailed);
        }

        [HttpGet("{language}/detailed/Top")]
        public async Task<ActionResult<ICollection<AnimePreview>>> GetAnimeDetailedAsync(string language)
        {
            var animeDetailed = await _animeDetailedAdapter.GetAnimesDetailedAsync(10, language);

            return Ok(animeDetailed);
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
        [HttpPost("add/seasonal")]
        public async Task<ActionResult<RelatedAnime>> AddSeasonalAsync([FromBody]RelatedAnimeDto relatedAnimeDto)
        {
            RelatedAnime relatedAnime = await _animeService.AddRelatedAnimeAsync(relatedAnimeDto);

            return Ok(relatedAnime);
        }
        [HttpPost("add/photo/{animeId}/{photoType}")]
        public async Task<ActionResult<Photo>> AddAnimePhoto(IFormFile file, int animeId, PhotoTypes photoType)
        {
            var photo = await _animeService.AddAnimePhotoAsync(file, animeId, photoType);

            return Ok(photo);
        }

        [HttpPost("add/comment/{animeId}")]
        public async Task<ActionResult<Comment>> AddCommentAsync([FromBody]CommentDto commentDto, int animeId)
        {
            CommentDto comment = await _animeService.AddAnimeComment(animeId, commentDto.Text!, commentDto.ParentCommentId);

            return Ok(comment);
        }

        [HttpPost("update/comment/{animeId}")]
        public async Task<ActionResult<Comment>> UpdateCommentAsync([FromBody] CommentDto commentDto, int animeId)
        {
            CommentDto comment = await _animeService.UpdateAnimeComment(animeId, commentDto.Id, commentDto.Text!);

            return Ok(comment);
        }

        [HttpDelete("delete/anime/{animeId}")]
        public async Task<IActionResult> DeleteAnimeAsync(int animeId)
        {
            await _animeService.DeleteAnimeAsync(animeId);

            return Ok("Successfully!");
        }

        [HttpDelete("delete/photo/{animeId}/{photoId}")]
        public async Task<IActionResult> DeletePhotoAsync(int animeId, int photoId)
        {
            await _animeService.DeleteAnimePhotoAsync(animeId, photoId);

            return Ok("Successfully!");
        }
        [HttpDelete("delete/comment/{animeId}/{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(int animeId, int commentId)
        {
            await _animeService.DeleteAnimeComment(animeId, commentId);

            return Ok("Successfully!");
        }
    }
}
