using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GubingTickets.Utilities.Cache
{
    public class CachingLayer : ICachingLayer
    {
        private readonly IDistributedCache _DistributedCache;

        public CachingLayer(IDistributedCache distributedCache)
        {
            _DistributedCache = distributedCache;
        }

        public TResult GetOrAddCacheValue<TResult>(Func<TResult> setValue, string cacheKey, int expirationMinutes = 30, bool isSlidingScale = false)
        {
            try
            {
                byte[] cachedValue = _DistributedCache.Get(GetCacheKey<TResult>(cacheKey));

                if (cachedValue != null)
                    return JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(cachedValue));

                TResult newCacheValue = setValue();

                DistributedCacheEntryOptions options = isSlidingScale ?
                    new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(expirationMinutes)) :
                    new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes));

                _DistributedCache.Set(GetCacheKey<TResult>(cacheKey), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newCacheValue)), options);

                return newCacheValue;
            }
            catch
            {
                return setValue();
            }
        }

        private string GetCacheKey<T>(string keyName)
        {
            return $"{typeof(T).FullName}_{keyName}".ToLower();
        }
    }
}
