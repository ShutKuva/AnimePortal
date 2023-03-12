using Core.DB;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AuthServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthServerContext(DbContextOptions<AuthServerContext> options) : base(options) { }
        public AuthServerContext()
        {

        }
    }
}