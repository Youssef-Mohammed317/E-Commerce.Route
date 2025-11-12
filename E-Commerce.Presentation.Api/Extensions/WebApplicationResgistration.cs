using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Api.Extensions
{
    public static class WebApplicationResgistration
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var storeDbContext = scope.ServiceProvider.GetService<StoreDbContext>();

            if (storeDbContext?.Database.GetPendingMigrations().Any() ?? false)

                storeDbContext.Database.Migrate();

            return app;
        }
        public static async Task<WebApplication> SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var DataInitializer = scope.ServiceProvider.GetService<IDataInitializer>();

            await  DataInitializer?.InitializeAsync()!;

            return app;
        }
    }
}
