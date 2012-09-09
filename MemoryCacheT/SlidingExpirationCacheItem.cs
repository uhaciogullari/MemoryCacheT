using System;

namespace MemoryCacheT
{
    public class SlidingExpirationCacheItem<TValue> : CacheItem<TValue>
    {
        private readonly TimeSpan _cacheInterval;
        private DateTime? _lastAccessDateTime;

        internal SlidingExpirationCacheItem(IDateTimeProvider dateTimeProvider, TValue value, TimeSpan cacheInterval)
            : base(dateTimeProvider, value)
        {
            _cacheInterval = cacheInterval;
            _lastAccessDateTime = dateTimeProvider.Now;
        }

        public SlidingExpirationCacheItem(TValue value, TimeSpan cacheInterval)
            : this(new DateTimeProvider(), value, cacheInterval) { }

        public override ICacheItem<TValue> CreateNewCacheItem(TValue value)
        {
            return new SlidingExpirationCacheItem<TValue>(value, _cacheInterval)
                {
                    OnExpire = OnExpire,
                    OnRemove = OnRemove
                };
        }

        public override TValue Value
        {
            get
            {
                _lastAccessDateTime = DateTimeProvider.Now;
                return CacheItemValue;
            }
        }

        public override bool IsExpired()
        {
            return DateTimeProvider.Now >= (_lastAccessDateTime + _cacheInterval);
        }
    }
}