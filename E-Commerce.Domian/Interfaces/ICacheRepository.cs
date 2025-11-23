using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Interfaces
{
    public interface ICacheRepository
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value, TimeSpan expiration = default);
        Task<bool> RemoveAsync(string key);
    }
}
