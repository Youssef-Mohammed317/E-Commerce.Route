using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Entites.ProductModule
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string PictureName { get; set; } = null!;

        #region Relations
        [ForeignKey(nameof(ProductBrand))]
        public Guid ProductBrandId { get; set; }

        public ProductBrand ProductBrand { get; set; } = null!;


        [ForeignKey(nameof(ProductType))]
        public Guid ProductTypeId { get; set; }

        public ProductType ProductType { get; set; } = null!;
        #endregion
    }
}
