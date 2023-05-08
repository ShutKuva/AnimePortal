using DAL;
using Microsoft.EntityFrameworkCore;

namespace AnimePortalAuthServer.Extensions
{
    public static class MigratingMiddlware
    {
        public static WebApplication UseMigration(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            AuthServerContext context = scope.ServiceProvider.GetRequiredService<AuthServerContext>();
            context.Database.Migrate();
            return app;
        }
    }
}
