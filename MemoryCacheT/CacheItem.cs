using System;

namespace MemoryCacheT
{
    public abstract class CacheItem<TValue> : ICacheItem<TValue>
    {
        internal IDateTimeProvider DateTimeProvider { get; private set; }
        protected TValue Value;

        internal CacheItem(IDateTimeProvider dateTimeProvider, TValue value)
        {
            DateTimeProvider = dateTimeProvider;
            Value = value;
        }

        public abstract TValue GetValue();

        public abstract bool IsExpired();

        public void Expire()
        {
            OnExpire(Value, DateTimeProvider.Now);
        }

        public void Remove()
        {
            OnRemove(Value, DateTimeProvider.Now);
        }

        public Action<TValue, DateTime> OnExpire { get; set; }

        public Action<TValue, DateTime> OnRemove { get; set; }

    }
}