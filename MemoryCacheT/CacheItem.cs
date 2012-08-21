using System;

namespace MemoryCacheT
{
    public abstract class CacheItem<TValue> : ICacheItem<TValue>
    {
        protected TValue Value;

        protected CacheItem(TValue value)
        {
            Value = value;
        }

        public abstract TValue GetValue(DateTime now);

        public abstract bool IsExpired(DateTime now);

        public void Expire(DateTime now)
        {
            OnExpire(Value, now);
        }

        public void Remove(DateTime now)
        {
            OnRemove(Value, now);
        }

        public Action<TValue, DateTime> OnExpire { get;set; }

        public Action<TValue, DateTime> OnRemove { get;set; }

    }
}