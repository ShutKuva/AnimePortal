﻿using Core.DB;

namespace DAL.Abstractions.Interfaces
{
    public interface IAnimeRepository : IRepository<Anime>
    {
        IQueryable<Anime> GetAnimeByCount(int count);
        IQueryable<Anime> GetAnimeByCount(int count, string language);
    }
}

