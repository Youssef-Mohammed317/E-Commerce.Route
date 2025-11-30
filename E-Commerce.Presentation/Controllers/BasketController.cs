using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBasketById([FromRoute] Guid id)
        {
            var result = await _basketService.GetBasketAsync(id);
            return FromResult(result);   // maps NotFound, etc.
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket([FromBody] BasketDTO basket)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);   // framework 400 with details

            var result = await _basketService.CreateOrUpdateBasketAsync(basket);
            return FromResult(result);                 // 200 or error ProblemDetails
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBasket([FromRoute] Guid id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            return FromResult(result);                 // 204 on success, error otherwise
        }
    }
}
