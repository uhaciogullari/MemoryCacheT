using System;

namespace MemoryCacheT
{
    public class AbsoluteExpirationCacheItem<TValue> : CacheItem<TValue>
    {
        private readonly DateTime _expirationDateTime;

        internal AbsoluteExpirationCacheItem(IDateTimeProvider dateTimeProvider, TValue value, DateTime expirationDateTime)
            : base(dateTimeProvider, value)
        {
            if (expirationDateTime < _dateTimeProvider.Now)
            {
                throw new ArgumentException("Expiration time must be greater than current time");
            }

            _expirationDateTime = expirationDateTime;
        }

        internal AbsoluteExpirationCacheItem(IDateTimeProvider dateTimeProvider, TValue value, TimeSpan cacheInterval)
            : base(dateTimeProvider, value)
        {
            _expirationDateTime = _dateTimeProvider.Now + cacheInterval;
        }

        public AbsoluteExpirationCacheItem(TValue value, DateTime expirationDate)
            : this(DateTimeProvider.Instance, value, expirationDate) { }

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
            get { return _dateTimeProvider.Now >= _expirationDateTime; }
        }
    }
}