using Core.DB;
using DAL.Abstractions.Interfaces;

namespace DAL.Repositories
{
    public class LocalizationRepository : GenericRepository<Localization>, ILocalizationRepository
    {
        public LocalizationRepository(AuthServerContext context) : base(context)
        {
        }
    }
}