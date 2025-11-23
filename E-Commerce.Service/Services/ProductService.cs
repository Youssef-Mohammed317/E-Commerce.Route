using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Commerce.Domian.Entites.ProductModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Service.Implementation.Exceptions;
using E_Commerce.Service.Implementation.Specifications;
using E_Commerce.Shared.DTOs;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginateResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams productQueryParams)
        {
            var spec = new ProductWithBrandAndTypeSpecification(productQueryParams);

            var repo = _unitOfWork.GetRepository<Product>();
            var products = await repo.GetAllAsync(spec);

            var data = _mapper.Map<IEnumerable<ProductDTO>>(products);

            var paginateResult = new PaginateResult<ProductDTO>
            {
                Page = productQueryParams.Page,
                PageSize = productQueryParams.PageSize,
                ItemsCount = data.Count(),
                Data = data,
                TotalCount = await repo.CountAsync(spec)
            };

            return paginateResult;
        }
        public async Task<IEnumerable<ProductBrandDTO>> GetAllProductBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand>().GetAllAsync();

            return _mapper.Map<IEnumerable<ProductBrandDTO>>(brands);
        }
        public async Task<IEnumerable<ProductTypeDTO>> GetAllProductTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType>().GetAllAsync();

            return _mapper.Map<IEnumerable<ProductTypeDTO>>(types);
        }
        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(spec) ;





            return _mapper.Map<ProductDTO>(product ?? throw new ProductNotFoundException(id));
        }

    }
}
