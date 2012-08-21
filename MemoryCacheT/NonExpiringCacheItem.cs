using System;

namespace MemoryCacheT
{
    public class NonExpiringCacheItem<TValue> : CacheItem<TValue>
    {
        public NonExpiringCacheItem(TValue value) : base(value) { }

        public override TValue GetValue(DateTime now)
        {
            return Value;
        }

        public override bool IsExpired(DateTime now)
        {
            return false;
        }

    }
}