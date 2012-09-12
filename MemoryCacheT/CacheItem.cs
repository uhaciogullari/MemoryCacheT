using System;

namespace MemoryCacheT
{
    public abstract class CacheItem<TValue> : ICacheItem<TValue>
    {
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly TValue _cacheItemValue;

        internal CacheItem(IDateTimeProvider dateTimeProvider, TValue value)
        {
            _dateTimeProvider = dateTimeProvider;
            _cacheItemValue = value;
        }

        public abstract ICacheItem<TValue> CreateNewCacheItem(TValue value);

        public abstract TValue Value { get; }

        public abstract bool IsExpired { get; }

        public void Expire()
        {
            if (OnExpire != null)
            {
                OnExpire(_cacheItemValue, _dateTimeProvider.Now);
            }
        }

        public void Remove()
        {
            if (OnRemove != null)
            {
                OnRemove(_cacheItemValue, _dateTimeProvider.Now);
            }
        }

        public Action<TValue, DateTime> OnExpire { get; set; }

        public Action<TValue, DateTime> OnRemove { get; set; }

    }
}