using System.Collections.Generic;

namespace MemoryCacheT
{
    public interface ICache<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        int Count { get; }
        bool IsEmpty { get; }

        ICollection<TKey> Keys { get; }
        ICollection<TValue> Values { get; }

        void Add(KeyValuePair<TKey, TValue> keyValuePair);
        void Add(TKey key, TValue value);
        void Add(TKey key, ICacheItem<TValue> cacheItem);

        bool TryAdd(TKey key, TValue value);
        bool TryAdd(TKey key, ICacheItem<TValue> cacheItem);
        
        bool TryGetValue(TKey key, out TValue value);
        bool TryGetValue(TKey key, out ICacheItem<TValue> cacheItem);
        
        bool TryUpdate(TKey key, TValue newValue);
        bool TryUpdate(TKey key, ICacheItem<TValue> newCacheItem);

        bool TryRemove(TKey key);
        bool TryRemove(TKey key,out TValue value);
        bool TryRemove(TKey key, out ICacheItem<TValue> value);
        void Clear();

        bool ContainsKey(TKey key);

    }
}