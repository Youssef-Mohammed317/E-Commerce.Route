using E_Commerce.Presentation.Attributes;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [RedisCache(15)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams productQueryParams)
        {
            var products = await _productService.GetAllProductsAsync(productQueryParams);
            return Ok(products);
        }
        [HttpGet("Brands")]
        [RedisCache]
        public async Task<ActionResult<IEnumerable<ProductBrandDTO>>> GetAllProductBrands()
        {
            var brands = await _productService.GetAllProductBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("Types")]
        [RedisCache]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetAllProductTypes()
        {
            var types = await _productService.GetAllProductTypesAsync();
            return Ok(types);
        }
        [HttpGet("{id:guid}")]
        [RedisCache]
        public async Task<ActionResult<ProductDTO>> GetProductById([FromRoute] Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            return Ok(product);
        }
    }
}
