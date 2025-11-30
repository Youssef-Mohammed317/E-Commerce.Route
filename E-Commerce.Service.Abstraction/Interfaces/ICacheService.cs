using System;
using System.Threading.Tasks;
using E_Commerce.Shared.Common;

namespace E_Commerce.Service.Abstraction.Interfaces
{
    public interface ICacheService
    {
        Task<Result<string>> GetAsync(string key);
        Task<Result> SetAsync(string key, object value, TimeSpan expiration = default);
        Task<Result> RemoveAsync(string key);
    }
}
