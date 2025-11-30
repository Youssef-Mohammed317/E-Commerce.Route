using AutoMapper;
using E_Commerce.Domian.Entites.BasketModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Service.Implementation.Exceptions;
using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<Result<BasketDTO>> CreateOrUpdateBasketAsync(
            BasketDTO basket,
            TimeSpan timeToLive = default)
        {
            var basketEntity = _mapper.Map<CustomerBasket>(basket);

            var result = await _basketRepository.CreateOrUpdateBasketAsync(
                basketEntity,
                timeToLive);

            if (result is null)
            {
                return Result<BasketDTO>.Fail(
                    Error.Failure(
                        code: "BASKET_SAVE_FAILED",
                        description: "Failed to create or update the basket."));
            }

            var dto = _mapper.Map<BasketDTO>(result);
            return Result<BasketDTO>.Ok(dto);
        }

        public async Task<Result> DeleteBasketAsync(Guid basketId)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(basketId);

            if (!deleted)
            {
                return Result.Fail(
                    Error.NotFound(
                        code: "BASKET_NOT_FOUND",
                        description: "Basket could not be deleted because it does not exist."));
            }

            return Result.Ok();
        }

        public async Task<Result<BasketDTO>> GetBasketAsync(Guid basketId)
        {
            var basketEntity = await _basketRepository.GetBasketAsync(basketId);

            if (basketEntity is null)
            {
                // Could also use a specific ErrorType.NotFound here
                return Result<BasketDTO>.Fail(
                    Error.NotFound(
                        code: "BASKET_NOT_FOUND",
                        description: "Basket with the specified ID was not found."));
            }

            var dto = _mapper.Map<BasketDTO>(basketEntity);
            return Result<BasketDTO>.Ok(dto);
        }
    }
}
