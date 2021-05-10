using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Utilities.Cache
{
    public interface ICachingLayer
    {
        TResult GetOrAddCacheValue<TResult>(Func<TResult> setValue, string cacheKey, int expirationMinutes = 30, bool isSlidingScale = false);
    }
}
