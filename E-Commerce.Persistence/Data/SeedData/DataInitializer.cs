using E_Commerce.Domian.Entites;
using E_Commerce.Domian.Entites.ProductModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.SeedData
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var hasProducts = _dbContext.Products.Any();
                var hasProductBrands = _dbContext.ProductBrands.Any();
                var hasProductTypes = _dbContext.ProductTypes.Any();


                if (hasProducts && hasProductBrands && hasProductTypes) return;

                var basePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "Files");

                if (!hasProductBrands)
                {
                    await SeedDataFromJson(Path.Combine(basePath, "brands.json"), _dbContext.ProductBrands);
                }
                if (!hasProductTypes)
                {
                    await SeedDataFromJson(Path.Combine(basePath, "types.json"), _dbContext.ProductTypes);
                }
                await _dbContext.SaveChangesAsync();
                if (!hasProducts)
                {
                    await SeedDataFromJson(Path.Combine(basePath, "products.json"), _dbContext.Products);
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task SeedDataFromJson<TEnitiy>(string filePath, DbSet<TEnitiy> entity) where TEnitiy : BaseEntity
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            try
            {
                //var jsonString = File.ReadAllText(filePath);

                using var reader = File.OpenRead(filePath);

                var data = await JsonSerializer.DeserializeAsync<IEnumerable<TEnitiy>>(reader, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (data is not null)
                {
                    await entity.AddRangeAsync(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }



        }
    }
}
