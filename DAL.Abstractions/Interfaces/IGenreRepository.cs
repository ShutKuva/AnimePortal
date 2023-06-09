﻿using Core.DB;

namespace DAL.Abstractions.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<ICollection<Genre>> GetAllGenressAsync();
    }
}
