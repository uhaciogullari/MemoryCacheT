namespace MemoryCacheT
{
    /// <summary>
    /// A cache item that does not expire.
    /// </summary>
    /// <typeparam name="TValue">Type of value in cache item.</typeparam>
    public class NonExpiringCacheItem<TValue> : CacheItem<TValue>
    {
        internal NonExpiringCacheItem(IDateTimeProvider dateTimeProvider, TValue value) : base(dateTimeProvider, value) { }

        /// <summary>
        /// Initializes a new instance of the NonExpiringCacheItem&lt;TValue&gt; class.
        /// </summary>
        /// <param name="value">Data for the cache item.</param>
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