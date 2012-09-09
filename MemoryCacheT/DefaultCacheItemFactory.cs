namespace MemoryCacheT
{
    internal class DefaultCacheItemFactory : ICacheItemFactory
    {
        public ICacheItem<TValue> CreateInstance<TValue>(TValue value)
        {
            return new NonExpiringCacheItem<TValue>(value);
        }
    }
}