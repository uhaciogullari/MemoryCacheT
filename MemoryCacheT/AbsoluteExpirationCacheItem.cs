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

        public override TValue Value
        {
            get { return CacheItemValue; }
        }

        public override bool IsExpired()
        {
            return DateTimeProvider.Now >= _expirationDate;
        }
    }
}