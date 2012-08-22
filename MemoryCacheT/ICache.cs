using System.Collections.Generic;

namespace MemoryCacheT
{
    public interface ICache<TKey, TValue>
    {
        int Count { get; }
        bool IsEmpty { get; }

        IEnumerable<TKey> Keys { get; }
        IEnumerable<TValue> Values { get; }

        void Clear();
        bool TryAdd(TKey key, ICacheItem<TValue> cacheItem);
        bool TryGetValue(TKey key, out TValue value);
    }
}