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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("User ID=yevhenii.zhuravel;Password=ec4WuUqB6fjb;Host=ep-dawn-cherry-183783.eu-central-1.aws.neon.tech;Database=neondb;Pooling=true;Connection Lifetime=0;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}