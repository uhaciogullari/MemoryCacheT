using System;

namespace MemoryCacheT
{
    public class SlidingExpirationCacheItem<TValue> : CacheItem<TValue>
    {
        private readonly TimeSpan _cacheInterval;
        private DateTime? _lastAccessDateTime;

        public SlidingExpirationCacheItem(TValue value, TimeSpan cacheInterval) : base(new DateTimeProvider(), value)
        {
            _cacheInterval = cacheInterval;
        }

        public override TValue GetValue()
        {
            throw new NotImplementedException();
        }

        public override bool IsExpired()
        {
            throw new NotImplementedException();
        }
    }
}