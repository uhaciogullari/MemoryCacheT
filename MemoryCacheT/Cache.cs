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

    }
}