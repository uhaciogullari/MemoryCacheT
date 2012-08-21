using System;

namespace MemoryCacheT
{
    public interface ICacheItem<TValue>
    {
        TValue GetValue(DateTime now);

        bool IsExpired(DateTime now);

        void Expire(DateTime now);
        void Remove(DateTime now);

        Action<TValue, DateTime> OnExpire { get; set; }
        Action<TValue, DateTime> OnRemove { get; set; }
    }
}