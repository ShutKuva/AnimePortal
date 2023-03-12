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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZHEKA;Database=AuthServer;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}