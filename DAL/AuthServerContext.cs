using Core.DB;
using Core.DTOs;
using Core.DTOs.Anime;
using Microsoft.EntityFrameworkCore;

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

    }
}