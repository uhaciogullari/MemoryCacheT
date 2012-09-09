using System;

namespace MemoryCacheT
{
    public class AbsoluteExpirationCacheItem<TValue> : CacheItem<TValue>
    {
        private readonly DateTime _expirationDate;

        internal AbsoluteExpirationCacheItem(IDateTimeProvider dateTimeProvider, TValue value, DateTime expirationDate)
            : base(dateTimeProvider, value)
        {
            _expirationDate = expirationDate;
        }

        public AbsoluteExpirationCacheItem(TValue value, DateTime expirationDate)
            : this(new DateTimeProvider(), value, expirationDate) { }

        public override ICacheItem<TValue> CreateNewCacheItem(TValue value)
        {
            return new AbsoluteExpirationCacheItem<TValue>(value, _expirationDate)
                {
                    OnExpire = OnExpire,
                    OnRemove = OnRemove
                };
        }

        public override TValue Value
        {
            get { return CacheItemValue; }
        }

        public override bool IsExpired
        {
            get { return DateTimeProvider.Now >= _expirationDate; }
        }
    }
}