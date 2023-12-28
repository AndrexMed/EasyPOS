using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
