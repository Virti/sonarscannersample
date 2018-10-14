using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace vtb.Core.Utils.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<TCachedObject> GetOrUpdate<TCachedObject>(this IDistributedCache cache, string key, Func<Task<TCachedObject>> func, TimeSpan? absoluteExpirationRelativeToNow = null)
        {
            var fromCache = await cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(fromCache))
            {
                // deserialize
                return JsonConvert.DeserializeObject<TCachedObject>(fromCache);
            }
            else
            {
                // invoke given func and store it' result in cache for future
                var result = await func.Invoke();
                var value = JsonConvert.SerializeObject(result);

                if (absoluteExpirationRelativeToNow.HasValue)
                {
                    await cache.SetStringAsync(key, value, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow });
                }
                else
                {
                    await cache.SetStringAsync(key, value);
                }

                return result;
            }
        }
    }
}
