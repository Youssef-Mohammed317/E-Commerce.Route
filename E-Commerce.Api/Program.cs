
using E_Commerce.Api.Factories;
using E_Commerce.Api.Middleware;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Data.Migrations;
using E_Commerce.Persistence.Data.SeedData;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Presentation.Api.Extensions;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Service.Implementation.MappingProfiles.ProductModule;
using E_Commerce.Service.Implementation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region IoC

            #region Database
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StoreDbConnection"))
                //.UseLazyLoadingProxies()
                ;
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse(
                    builder.Configuration.GetConnectionString("RedisConnection")!
                );

                options.Ssl = false; // ❗ مهم جدًا
                options.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(options);
            });
            #endregion

            #region Services
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddAutoMapper(config =>
            {
                config.AddMaps(typeof(ProductModuleProfile).Assembly);
            });
            builder.Services.AddTransient<ProductPictureUrlResolver>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                //options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });

            #endregion

            #endregion



            var app = builder.Build();

            #region Data-Seed & Apply Migrations

            await app.MigrateDatabaseAsync();
            await app.SeedDataAsync();

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
