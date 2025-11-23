using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction.Interfaces
{
    public interface ICacheService
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, object value, TimeSpan expiration = default);
        Task<bool> RemoveAsync(string key);
    }
}
