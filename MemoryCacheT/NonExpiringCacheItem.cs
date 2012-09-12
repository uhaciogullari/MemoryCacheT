namespace MemoryCacheT
{
    public class NonExpiringCacheItem<TValue> : CacheItem<TValue>
    {
        internal NonExpiringCacheItem(IDateTimeProvider dateTimeProvider, TValue value) : base(dateTimeProvider, value) { }

        public NonExpiringCacheItem(TValue value) : this(DateTimeProvider.Instance, value) { }

        public override ICacheItem<TValue> CreateNewCacheItem(TValue value)
        {
            return new NonExpiringCacheItem<TValue>(value)
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
            get { return false; }
        }
    }
}