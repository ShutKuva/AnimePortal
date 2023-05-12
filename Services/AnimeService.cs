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

        public async Task CreateAsync(AnimeDto? animeDto)
        {
            if (animeDto == null)
            {
                throw new ArgumentNullException(nameof(animeDto), "Anime cannot be null");
            }
            if (animeDto.AnimeDescription == null)
            {
                throw new ArgumentNullException(nameof(animeDto.AnimeDescription), "Anime description cannot be null");
            }

            Anime? anime = await GetAnimeByNameAsync(animeDto.AnimeDescription.FirstOrDefault().Title);
            if (anime != null)
            {
                throw new InvalidOperationException($"Anime with Title { animeDto.AnimeDescription.FirstOrDefault().Title} already exists.");
            }

            anime = _mapper.Map<Anime>(animeDto);
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

        public Task<Anime> UpdateAnimeAsync(AnimeDto animeDto, int animeId)
        {
            var anime = _mapper.Map<Anime>(animeDto);
            anime.Id = animeId;
            return UpdateAnimeAsync(anime);
        }

        public async Task DeleteAnimeAsync(int animeId)
        {
            Anime anime = await GetAnimeAsync(animeId);

            await _uow.AnimeRepository.DeleteAsync(animeId);

            foreach (var photo in anime.Photos!)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(photo.Id);
                }
                catch
                {
                    // ignored
                }
            }

            await _uow.SaveChangesAsync();
        }

        public async Task<Photo> AddAnimePhotoAsync(IFormFile file, int animeId, PhotoTypes photoType = PhotoTypes.Screenshots)
        {
            Anime anime = await GetAnimeAsync(animeId);
            var photo = await _photoService.UploadPhotoAsync(file);

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
    }
}
