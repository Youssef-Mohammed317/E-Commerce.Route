using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction.Interfaces
{
    public interface IBasketService
    {
        Task<Result<BasketDTO>> GetBasketAsync(Guid basketId);

        Task<Result<BasketDTO>> CreateOrUpdateBasketAsync(
            BasketDTO basket,
            TimeSpan timeToLive = default);

        Task<Result> DeleteBasketAsync(Guid basketId);
    }

}
