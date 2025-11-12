
using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Data.SeedData;
using E_Commerce.Presentation.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Database
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StoreDbConnection"));
            });
            #endregion

            #region IoC

            builder.Services.AddScoped<IDataInitializer, DataInitializer>();

            #endregion



            var app = builder.Build();

            #region Data-Seed ApplyMigrations

            app.MigrateDatabase();
            app.SeedData();

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
