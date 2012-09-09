using System;

namespace MemoryCacheT
{
    public interface ICacheItem<TValue>
    {
        TValue Value { get; }
        
        ICacheItem<TValue> CreateNewCacheItem(TValue value);

        bool IsExpired { get; }

        void Expire();
        void Remove();

        Action<TValue, DateTime> OnExpire { get; set; }
        Action<TValue, DateTime> OnRemove { get; set; }
    }
}