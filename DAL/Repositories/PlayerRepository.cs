using Core.DB;
using DAL.Abstractions.Interfaces;

namespace DAL.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(AuthServerContext context) : base(context)
        {
        }
    }
}