using E_Commerce.Domian.Interfaces;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.Common;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<string>> GetAsync(string key)
        {
            var value = await _cacheRepository.GetAsync(key);

            if (value is null)
            {
                return Result<string>.Fail(
                    Error.NotFound(
                        code: "CACHE_KEY_NOT_FOUND",
                        description: "The requested cache entry was not found."));
            }

            return Result<string>.Ok(value);
        }

        public async Task<Result> RemoveAsync(string key)
        {
            var removed = await _cacheRepository.RemoveAsync(key);

            if (!removed)
            {
                return Result.Fail(
                    Error.NotFound(
                        code: "CACHE_KEY_NOT_FOUND",
                        description: "The cache entry to remove does not exist."));
            }

            return Result.Ok();
        }

        public async Task<Result> SetAsync(string key, object value, TimeSpan expiration = default)
        {
            var jsonString = JsonSerializer.Serialize(value);
            await _cacheRepository.SetAsync(key, jsonString, expiration);

            return Result.Ok();
        }
    }
}
