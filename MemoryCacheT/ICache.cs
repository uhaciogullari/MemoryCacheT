using System.Collections.Generic;

namespace MemoryCacheT
{
    public interface ICache<TKey, TValue> : IDictionary<TKey, TValue>
    {
        void Add(TKey key, ICacheItem<TValue> cacheItem);

        bool TryAdd(TKey key, TValue value);
        bool TryAdd(TKey key, ICacheItem<TValue> cacheItem);
        
        bool TryGetValue(TKey key, out ICacheItem<TValue> cacheItem);
        
        bool TryUpdate(TKey key, TValue newValue);
        bool TryUpdate(TKey key, ICacheItem<TValue> newCacheItem);

        bool Remove(TKey key, out TValue value);
        bool Remove(TKey key, out ICacheItem<TValue> cacheItem);
    }
}