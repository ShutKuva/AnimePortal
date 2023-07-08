using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(AuthServerContext context) : base(context)
        {
        }

        public async Task<ICollection<Language>> GetAllLanguagesAsync()
        {
           return await context.Languages.ToListAsync();
        }
    }
}
