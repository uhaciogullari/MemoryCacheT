using System;

namespace MemoryCacheT
{
    /// <summary>
    /// A cache item that expires if it has not been accessed in a given span of time.
    /// </summary>
    /// <typeparam name="TValue">Type of value in cache item.</typeparam>
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

        /// <summary>
        /// Initializes a new instance of the SlidingExpirationCacheItem&lt;TValue&gt; class.
        /// </summary>
        /// <param name="value">Data for the cache item.</param>
        /// <param name="cacheInterval">Interval that will be used to determine if cache item has expired.</param>
        public SlidingExpirationCacheItem(TValue value, TimeSpan cacheInterval)
            : this(DateTimeProvider.Instance, value, cacheInterval) { }

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
                _lastAccessDateTime = _dateTimeProvider.Now;
                return _cacheItemValue;
            }
        }

        public override bool IsExpired
        {
            get { return _dateTimeProvider.Now >= (_lastAccessDateTime + _cacheInterval); }
        }
    }
}