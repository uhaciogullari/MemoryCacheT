using System;

namespace MemoryCacheT
{
    public class AbsoluteExpirationCacheItem<TValue>:CacheItem<TValue>
    {
        private readonly DateTime _expirationDate;

        public AbsoluteExpirationCacheItem(TValue value,DateTime expirationDate) : base(value)
        {
            _expirationDate = expirationDate;
        }

        public override TValue GetValue(DateTime now)
        {
            return Value;
        }

        public override bool IsExpired(DateTime now)
        {
            return now >= _expirationDate;
        }
    }
}