using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;
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
        private readonly IMapper _mapper;


        public AnimeService(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task CreateAsync(Anime? anime)
        {
            if (anime == null)
            {
                throw new ArgumentNullException(nameof(anime), "Anime cannot be null");
            }
            await _uow.AnimeRepository.CreateAsync(anime);
            await _uow.SaveChangesAsync();
        }
        public async Task<Anime> GetAnimeAsync(int animeId)
        {
            var anime = await _uow.AnimeRepository.ReadAsync(animeId) ??
                        throw new NotFoundException($"Resource with id {animeId} was not found.");

            anime.Photos ??= new List<Photo>();
            return anime;
        }

        public async Task<AnimePreview> GetAnimePreviewAsync(int animeId)
        {
            var anime = await _uow.AnimeRepository.ReadAsync(animeId) ?? throw new NotFoundException($"Resource with id {animeId} was not found.");


            var animePreview = _mapper.Map<AnimePreview>(anime);
            return animePreview;
        }

        public Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quantity)
        {
            var animes = _uow.AnimeRepository.GetAnimeByCount(quantity) ?? throw new NotFoundException("For this query, nothing was found"); ;

            if (!animes.Any())
            {
                throw new NotFoundException("For this query, nothing was found");
            }
            var animePreviews = _mapper.Map<ICollection<AnimePreview>>(animes);

            return Task.FromResult(animePreviews);
        }

        public async Task<Anime> UpdateAnimeAsync(Anime anime)
        {
            await _uow.AnimeRepository.UpdateAsync(anime);
            await _uow.SaveChangesAsync();
            return anime;
        }
        public async Task DeleteAnimeAsync(int animeId)
        {
            Anime anime = await GetAnimeAsync(animeId);

            await _uow.AnimeRepository.DeleteAsync(animeId);

            foreach (var photo in anime.Photos!)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(photo.PublicId);
                }
                catch
                {
                    continue;
                }

            }

            await _uow.SaveChangesAsync();
        }

        public async Task<Photo> AddAnimePhotoAsync(IFormFile file, int animeId, PhotoTypes photoType = PhotoTypes.Screenshots)
        {
            var result = await _photoService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                throw new InvalidOperationException(result.Error.Message);
            }

            var anime = await GetAnimeAsync(animeId);
            var photo = new Photo()
            {
                ImageUrl = result.Url.AbsoluteUri,
                Title = result.OriginalFilename,
                PublicId = result.PublicId
            };

            anime.Photos?.Add(photo);
            await UpdateAnimeAsync(anime!);

            return photo;
        }

        public async Task DeleteAnimePhotoAsync(int animeId, int photoId)
        {
            var anime = await GetAnimeAsync(animeId);
            var photo = anime.Photos!.FirstOrDefault(p => p.Id == photoId) ?? throw new NotFoundException($"Resource with id {photoId} was not found."); ;

            anime.Photos?.Remove(photo);
            await _photoService.DeletePhotoAsync(photo.PublicId);
            await UpdateAnimeAsync(anime);
        }


    }
}
