using System;

namespace MemoryCacheT
{
    public interface ICacheItem<TValue>
    {
        TValue GetValue();

        bool IsExpired();

        void Expire();
        void Remove();

        Action<TValue, DateTime> OnExpire { get; set; }
        Action<TValue, DateTime> OnRemove { get; set; }
    }
}