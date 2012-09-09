using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MemoryCacheT
{
    internal class ConcurrentDictionaryAdapter<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IConcurrentDictionary<TKey, TValue>
    {
        internal ConcurrentDictionaryAdapter(IEqualityComparer<TKey> keyEqualityComparer)
            : base(keyEqualityComparer) { }

        public ConcurrentDictionaryAdapter() { }

    }
}