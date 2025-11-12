using E_Commerce.Domian.Entites.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        #region Constructors
        public StoreDbContext()
        {
        }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        #endregion

        #region Config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
        #endregion

        #region Modules

        #region ProductModule
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        #endregion

        #region MyRegion

        #endregion

        #endregion

    }
}
