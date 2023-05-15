using Core.DB;
using Core.DTOs;
using Core.DTOs.Anime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL
{
    public class AuthServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public AuthServerContext(DbContextOptions<AuthServerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>()
                .Property(a => a.Tags)
                .HasConversion(
                    tags => string.Join(",", tags),
                    tagsString => tagsString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    new ValueComparer<ICollection<string>>((c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToHashSet())
                );
        }
    }
}