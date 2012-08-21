using System;

namespace MemoryCacheT
{
    public class SlidingExpirationCacheItem<TValue> : CacheItem<TValue>
    {
        private readonly TimeSpan _cacheInterval;
        private DateTime? _lastAccessDateTime;

        public SlidingExpirationCacheItem(TValue value, TimeSpan cacheInterval) : base(value)
        {
            _cacheInterval = cacheInterval;
        }

        public override TValue GetValue(DateTime now)
        {
            throw new NotImplementedException();
        }

        public override bool IsExpired(DateTime now)
        {
            throw new NotImplementedException();
        }
    }
}