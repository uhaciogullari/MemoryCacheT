using System;

namespace MemoryCacheT
{
    public abstract class CacheItem<TValue> : ICacheItem<TValue>
    {
        internal IDateTimeProvider DateTimeProvider { get; private set; }
        protected TValue CacheItemValue;

        internal CacheItem(IDateTimeProvider dateTimeProvider, TValue value)
        {
            DateTimeProvider = dateTimeProvider;
            CacheItemValue = value;
        }

        public abstract TValue Value { get; }

        public abstract bool IsExpired();

        public void Expire()
        {
            OnExpire(CacheItemValue, DateTimeProvider.Now);
        }

        public void Remove()
        {
            OnRemove(CacheItemValue, DateTimeProvider.Now);
        }

        public Action<TValue, DateTime> OnExpire { get; set; }

        public Action<TValue, DateTime> OnRemove { get; set; }

    }
}