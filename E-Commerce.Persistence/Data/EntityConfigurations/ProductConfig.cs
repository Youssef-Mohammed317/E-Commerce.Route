using E_Commerce.Domian.Entites.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.EntityConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Description).HasColumnType("nvarchar(500)")
                .HasMaxLength(500);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.PictureUrl).HasColumnType("nvarchar(200)")
                .HasMaxLength(200);

            builder.Property(p => p.PictureName)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.HasOne(p => p.ProductBrand)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.ProductBrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProductType)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
