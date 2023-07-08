using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AuthServerContext context) : base(context)
        {
        }

        public async Task<ICollection<Genre>> GetAllGenressAsync()
        {
            return await context.Genres.ToListAsync();
        }
    }
}
