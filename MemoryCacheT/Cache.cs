using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace MemoryCacheT
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private class TimerAdapter : Timer, ITimer { }

        private readonly ITimer _timer;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ConcurrentDictionary<TKey, ICacheItem<TValue>> _cachedItems;

        internal Cache(ITimer timer, IDateTimeProvider dateTimeProvider, TimeSpan timerInterval, IEqualityComparer<TKey> keyEqualityComparer = null)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }

            if (timerInterval.TotalMilliseconds <= default(double))
            {
                throw new ArgumentException("Timer interval should be greater then zero miliseconds");
            }

            _timer = timer;
            _dateTimeProvider = dateTimeProvider;
            _timer.Interval = timerInterval.TotalMilliseconds;
            _cachedItems = keyEqualityComparer != null
                              ? new ConcurrentDictionary<TKey, ICacheItem<TValue>>(keyEqualityComparer)
                              : new ConcurrentDictionary<TKey, ICacheItem<TValue>>();

            _timer.Elapsed += CheckExpiredItems;
            _timer.Start();
        }

        public Cache(TimeSpan timerInterval, IEqualityComparer<TKey> keyEqualityComparer = null)
            : this(new TimerAdapter(), new DateTimeProvider(), timerInterval, keyEqualityComparer)
        { }

        private void CheckExpiredItems(object sender, ElapsedEventArgs e)
        {
            IEnumerable<TKey> expiredItemKeys = _cachedItems.Where(item => item.Value.IsExpired()).Select(item => item.Key);

            foreach (TKey expiredItemKey in expiredItemKeys)
            {
                ICacheItem<TValue> expiredItem;
                if (_cachedItems.TryRemove(expiredItemKey, out expiredItem))
                {
                    expiredItem.Expire();
                }
            }
        }

        public int Count
        {
            get { return _cachedItems.Count; }
        }

        public bool IsEmpty
        {
            get { return _cachedItems.IsEmpty; }
        }

        public IEnumerable<TKey> Keys
        {
            get { return _cachedItems.Keys; }
        }

        public IEnumerable<TValue> Values
        {
            get { return _cachedItems.Values.Select(item => item.Value); }
        }

        public void Clear()
        {
            _cachedItems.Clear();
        }

        public bool TryAdd(TKey key, ICacheItem<TValue> cacheItem)
        {
            if(cacheItem ==null)
            {
                throw new ArgumentNullException("cacheItem");
            }

            return _cachedItems.TryAdd(key, cacheItem);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            ICacheItem<TValue> cacheItemValue;

            bool result = _cachedItems.TryGetValue(key, out cacheItemValue);

            if(result)
            {
                value = cacheItemValue.Value;
            }

            return result;
        }
    }
}