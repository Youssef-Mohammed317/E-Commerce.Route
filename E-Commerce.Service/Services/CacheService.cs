using E_Commerce.Domian.Interfaces;
using E_Commerce.Service.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<string> GetAsync(string key)
        {

            return await _cacheRepository.GetAsync(key);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _cacheRepository.RemoveAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan expiration = default)
        {
            var jsonString = JsonSerializer.Serialize(value);
            await _cacheRepository.SetAsync(key, jsonString, expiration);
        }
    }
}
