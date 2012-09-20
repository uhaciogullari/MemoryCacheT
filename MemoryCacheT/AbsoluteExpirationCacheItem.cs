using System;

namespace MemoryCacheT
{
    /// <summary>
    /// A cache item that expires after a certain duration or specified time in the future.
    /// </summary>
    /// <typeparam name="TValue">Type of value in cache item.</typeparam>
    public class AbsoluteExpirationCacheItem<TValue> : CacheItem<TValue>
    {
        private readonly DateTime _expirationDateTime;

        internal AbsoluteExpirationCacheItem(IDateTimeProvider dateTimeProvider, TValue value, DateTime expirationDateTime)
            : base(dateTimeProvider, value)
        {
            DateTime utcExpirationDateTime = expirationDateTime.ToUniversalTime();
            if (utcExpirationDateTime < _dateTimeProvider.UtcNow)
            {
                throw new ArgumentException("Expiration time must be greater than current time");
            }

            _expirationDateTime = utcExpirationDateTime;
        }

        internal AbsoluteExpirationCacheItem(IDateTimeProvider dateTimeProvider, TValue value, TimeSpan cacheInterval)
            : base(dateTimeProvider, value)
        {
            _expirationDateTime = _dateTimeProvider.UtcNow + cacheInterval;
        }

        /// <summary>
        /// Initializes a new instance of the AbsoluteExpirationCacheItem&lt;TValue&gt; class.
        /// </summary>
        /// <param name="value">Data for the cache item.</param>
        /// <param name="expirationDate">Expiration time for the cache item.</param>
        /// <exception cref="ArgumentException">expirationDate is a time in the past.</exception>
        public AbsoluteExpirationCacheItem(TValue value, DateTime expirationDate)
            : this(DateTimeProvider.Instance, value, expirationDate) { }

        /// <summary>
        /// Initializes a new instance of the AbsoluteExpirationCacheItem&lt;TValue&gt; class.
        /// </summary>
        /// <param name="value">Data for the cache item.</param>
        /// <param name="cacheInterval">Interval before the cache item will expire, beginning from now.</param>
        public AbsoluteExpirationCacheItem(TValue value, TimeSpan cacheInterval)
            : this(DateTimeProvider.Instance, value, cacheInterval) { }

        public override ICacheItem<TValue> CreateNewCacheItem(TValue value)
        {
            return new AbsoluteExpirationCacheItem<TValue>(_dateTimeProvider, value, _expirationDateTime)
                {
                    OnExpire = OnExpire,
                    OnRemove = OnRemove
                };
        }

        public override TValue Value
        {
            get { return _cacheItemValue; }
        }

        public override bool IsExpired
        {
            get { return _dateTimeProvider.UtcNow >= _expirationDateTime; }
        }
    }
}