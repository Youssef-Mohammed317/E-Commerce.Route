using AutoMapper;
using E_Commerce.Domian.Entites.BasketModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Service.Implementation.Exceptions;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<BasketDTO?> CreateOrUpdateBasketAsync(BasketDTO basket, TimeSpan timeToLive = default)
        {
            var basketEntity = _mapper.Map<CustomerBasket>(basket);

            var result = await _basketRepository.CreateOrUpdateBasketAsync(basketEntity, timeToLive);

            return _mapper.Map<BasketDTO>(result);
        }

        public async Task<bool> DeleteBasketAsync(Guid basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }

        public async Task<BasketDTO?> GetBasketAsync(Guid basketId)
        {
            var basketEntity = await _basketRepository.GetBasketAsync(basketId);

            return _mapper.Map<BasketDTO>(basketEntity ?? throw new BasketNotFoundException(basketId));
        }
    }
}
