using E_Commerce.Domian.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }


        public async Task<string> GetAsync(string key)
        {
            return (await _database.StringGetAsync(key))!;   
        }

        public async Task<bool> RemoveAsync(string key)
        {
          return  await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync(string key, string value, TimeSpan expiration = default)
        {

            await _database.StringSetAsync(key, value, expiration == default ? TimeSpan.FromDays(1) : expiration);
        }
    }
}
