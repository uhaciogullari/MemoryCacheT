using System.Collections.Generic;

namespace MemoryCacheT
{
    internal interface IConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        bool IsEmpty { get; }
        bool TryAdd(TKey key, TValue value);
        bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue);
        bool TryRemove(TKey key, out TValue removedItem);
    }
}