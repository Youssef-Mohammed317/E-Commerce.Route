using E_Commerce.Service.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _expirationMinutes;
        public RedisCacheAttribute(int expirationMinutes = 10)
        {
            _expirationMinutes = expirationMinutes;
        }
        //public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    /* redis key => request | value => data | expiration time
        //    if data is in cache
        //        return cached data and skip action execution
        //    else
        //        proceed to action execution
        //        store result in cache if success response
        //    */

        //    var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

        //    var cacheKey = GenerateCacheKey(context);

        //    var cachedDataTask = await cacheService.GetAsync(cacheKey);

        //    if (cachedDataTask != null)
        //    {
        //        context.Result = new OkObjectResult(cachedDataTask);
        //        return;
        //    }
        //    var executedContext = await next();
        //    if (executedContext.Result is OkObjectResult okResult)
        //    {
        //        await cacheService.SetAsync(cacheKey, okResult.Value!, TimeSpan.FromMinutes(_expirationMinutes));
        //    }
        //}

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices
                .GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKey(context);

            var cacheResult = await cacheService.GetAsync(cacheKey);

            // cache hit
            if (cacheResult.IsSuccess && cacheResult.HasValue)
            {
                // cached JSON -> object
                var cachedJson = cacheResult.Value;
                var cachedObject = System.Text.Json.JsonSerializer.Deserialize<object>(cachedJson!);

                context.Result = new OkObjectResult(cachedObject);
                return;
            }

            // cache miss -> execute action
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okResult)
            {
                await cacheService.SetAsync(
                    cacheKey,
                    okResult.Value!,
                    TimeSpan.FromMinutes(_expirationMinutes));
            }
        }

        private string GenerateCacheKey(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
