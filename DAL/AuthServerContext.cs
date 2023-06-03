using Core.DB;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AuthServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Language> Languages { get; set; }
        public AuthServerContext(DbContextOptions<AuthServerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeDescription>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.AnimeDescriptions);
        }
    }
}