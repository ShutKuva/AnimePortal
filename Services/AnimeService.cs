using AutoMapper;
using CloudinaryDotNet.Actions;
using Core.DB;
using Core.DTOs.Anime;
using Core.DTOs.Others;
using Core.Enums;
using Core.Exceptions;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.Abstraction;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPhotoService _photoService;
        private readonly ILanguageService _languageService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public AnimeService(IUnitOfWork uow, IMapper mapper, IPhotoService photoService, ILanguageService languageService, ICommentService commentService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
            _languageService = languageService;
            _commentService = commentService;
        }

        public async Task CreateAsync(AnimeDto? animeDto)
        {
            if (animeDto == null)
            {
                throw new ArgumentNullException(nameof(animeDto), "Anime cannot be null");
            }
            if (!animeDto.AnimeDescription.Any())
            {
                throw new ArgumentNullException(nameof(animeDto.AnimeDescription), "Anime description cannot be null");
            }

            Anime anime = await MapAnimeDtoToAnime(animeDto);

            await _uow.AnimeRepository.CreateAsync(anime);
            await _uow.SaveChangesAsync();
        }

        public async Task<Anime> GetAnimeAsync(int animeId)
        {
            Anime anime = await _uow.AnimeRepository.ReadAsync(animeId) ??
                        throw new NotFoundException($"Resource with id {animeId} was not found.");

            return anime;
        }

        public Task<IQueryable<Anime>> GetAnimeByCountAsync(int quantity)
        {
            IQueryable<Anime> animes = _uow.AnimeRepository.GetAnimeByCount(quantity) ??
                                       throw new NotFoundException("For this query, nothing was found");

            return Task.FromResult(animes);
        }

        public Task<IQueryable<Anime>> GetAnimeByCountAsync(int quantity, string language)
        {
            var animes = _uow.AnimeRepository.GetAnimeByCount(quantity, language);

            if (!animes.Any())
            {
                throw new NotFoundException("For this query, nothing was found");
            }

            return Task.FromResult(animes);
        }

        public async Task<Anime> UpdateAnimeAsync(Anime anime)
        {
            if (anime == null)
            {
                throw new ArgumentNullException(nameof(anime), "Anime cannot be null");
            }

            await _uow.AnimeRepository.UpdateAsync(anime);
            await _uow.SaveChangesAsync();

            return anime;
        }

        public async Task<Anime> UpdateAnimeAsync(AnimeDto animeDto, int animeId)
        {
            var anime = _mapper.Map<Anime>(animeDto);
            anime.Id = animeId;
            return await UpdateAnimeAsync(anime);
        }

        public async Task<CommentDto> AddAnimeComment(int animeId, string text, int parentCommentId = 0)
        {
            Anime anime = await GetAnimeAsync(animeId);
            
            Comment comment = await _commentService.CreateCommentAsync(new(){Text = text, ParentCommentId = parentCommentId});
            anime.Comments.Add(comment);

            await _uow.SaveChangesAsync();

            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            return commentDto;
        }

        public async Task<CommentDto> ChangeAnimeComment(int animeId, int commentId, string text)
        {
            Anime anime = await GetAnimeAsync(animeId);
            Comment comment = await _commentService.UpdateCommentAsync(commentId, text);

            await _uow.SaveChangesAsync();
            return _mapper.Map<CommentDto>(comment);
        }


        public async Task RemoveAnimeComment(int animeId, int commentId)
        {
            Comment comment = await _commentService.GetCommentAsync(commentId);
            Anime anime = await GetAnimeAsync(animeId);

            anime.Comments.Remove(comment);
            await _commentService.DeleteCommentAsync(commentId);

            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAnimeAsync(int animeId)
        {
            Anime anime = await GetAnimeAsync(animeId);

            List<Task<DeletionResult>> tasks = new List<Task<DeletionResult>>(); 
            foreach (var photo in anime.Photos!)
            {
                try
                {
                    tasks.Add(_photoService.DeletePhotoAsync(photo.Id));
                }
                catch
                {
                    // ignored
                }
            }

            await Task.WhenAll(tasks);
            await _uow.AnimeRepository.DeleteAsync(animeId);

            await _uow.SaveChangesAsync();
        }

        public async Task<Photo> AddAnimePhotoAsync(IFormFile file, int animeId, PhotoTypes photoType = PhotoTypes.Screenshots)
        {
            Anime anime = await GetAnimeAsync(animeId);
            var photo = await _photoService.UploadPhotoAsync(file, photoType);

            anime.Photos!.Add(photo);
            await _uow.SaveChangesAsync();

            return photo;
        }

        public async Task DeleteAnimePhotoAsync(int animeId, int photoId)
        {
            Anime anime = await GetAnimeAsync(animeId);
            bool photoIsExist = anime.Photos!.Any(p => p.Id == photoId);
            if (!photoIsExist)
            {
                throw new NotFoundException($"Resource with id {photoId} was not found in anime with {animeId}.");
            }

            await _photoService.DeletePhotoAsync(photoId);
        }

        private async Task<Anime?> GetAnimeByNameAsync(string animeName)
        {
            var anime = await _uow.AnimeRepository.ReadByConditionAsync(a => a.AnimeDescriptions.FirstOrDefault()!.Title == animeName);
            return anime.FirstOrDefault();
        }

        private async Task<Anime> MapAnimeDtoToAnime(AnimeDto animeDto)
        {
            Anime anime = _mapper.Map<Anime>(animeDto);
            foreach (var aD in anime.AnimeDescriptions)
            {
                var animeCheck = await GetAnimeByNameAsync(aD!.Title);
                if (animeCheck != null)
                {
                    throw new InvalidOperationException($"Anime with Title {aD.Title} already exists.");
                }

                var language = await _languageService.GetLanguageByNameAsync(aD.Language!.Name);
                if (language != null)
                {
                    aD.Language = language;
                }
            }

            return anime ?? throw new InvalidOperationException();
        }

    }
}
