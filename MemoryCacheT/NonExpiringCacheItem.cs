namespace MemoryCacheT
{
    public class NonExpiringCacheItem<TValue> : CacheItem<TValue>
    {
        internal NonExpiringCacheItem(IDateTimeProvider dateTimeProvider, TValue value) : base(dateTimeProvider, value) { }

        public NonExpiringCacheItem(TValue value) : this(new DateTimeProvider(), value) { }

        public override ICacheItem<TValue> CreateNewCacheItem(TValue value)
        {
            return new NonExpiringCacheItem<TValue>(value);
        }

        public override TValue Value
        {
            get { return CacheItemValue; }
        }

        public override bool IsExpired()
        {
            return false;
        }



    }
}