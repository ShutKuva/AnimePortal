using Core.DB;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AuthServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Anime> Animes { get; set; }

        public AuthServerContext(DbContextOptions<AuthServerContext> options) : base(options) { }
    }
}