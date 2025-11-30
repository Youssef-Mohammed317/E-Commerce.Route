using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction.Interfaces
{
    public interface IProductService
    {
        Task<Result<PaginateResult<ProductDTO>>> GetAllProductsAsync(
            ProductQueryParams productQueryParams);

        Task<Result<IEnumerable<ProductBrandDTO>>> GetAllProductBrandsAsync();

        Task<Result<IEnumerable<ProductTypeDTO>>> GetAllProductTypesAsync();

        Task<Result<ProductDTO>> GetProductByIdAsync(Guid id);
    }

}
