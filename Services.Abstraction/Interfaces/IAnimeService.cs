﻿using Core.DB;
using Core.DTOs.Anime;
using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Services.Abstraction.Interfaces
{
    public interface IAnimeService
    {
        Task CreateAsync(AnimeDto? anime);
        Task<Anime> GetAnimeAsync(int animeId);
        Task<IQueryable<Anime>> GetAnimeByCountAsync(int quantity);
        Task<IQueryable<Anime>> GetAnimeByCountAsync(int quantity, string language);
        Task<Anime> UpdateAnimeAsync(Anime anime);
        Task<Anime> UpdateAnimeAsync(AnimeDto animeDto, int animeId);
        Task<Photo> AddAnimePhotoAsync(IFormFile file, int animeId, PhotoTypes photoType = PhotoTypes.Screenshots);
        Task DeleteAnimeAsync(int animeId);
        Task DeleteAnimePhotoAsync(int animeId, int photoId);

    }
}
