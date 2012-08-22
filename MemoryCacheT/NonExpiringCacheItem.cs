namespace MemoryCacheT
{
    public class NonExpiringCacheItem<TValue> : CacheItem<TValue>
    {
        internal NonExpiringCacheItem(IDateTimeProvider dateTimeProvider, TValue value) : base(dateTimeProvider, value) { }

        public NonExpiringCacheItem(TValue value) : this(new DateTimeProvider(), value) { }

        public override TValue GetValue()
        {
            return Value;
        }

        public override bool IsExpired()
        {
            return false;
        }

    }
}