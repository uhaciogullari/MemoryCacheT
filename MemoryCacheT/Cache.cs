using System;
using System.Collections;
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
        private readonly ICacheItemFactory _cacheItemFactory;
        private readonly IConcurrentDictionary<TKey, ICacheItem<TValue>> _cachedItems;

        internal Cache(ITimer timer,
                       IDateTimeProvider dateTimeProvider,
                       TimeSpan timerInterval,
                       IEqualityComparer<TKey> keyEqualityComparer = null,
                       ICacheItemFactory cacheItemFactory = null)
        {
            if (timer == null)
            {
                throw new ArgumentNullException("timer");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }

            if (timerInterval.TotalMilliseconds <= double.Epsilon)
            {
                throw new ArgumentException("Timer interval should be greater then zero miliseconds");
            }

            _timer = timer;
            _dateTimeProvider = dateTimeProvider;
            _timer.Interval = timerInterval.TotalMilliseconds;
            _cachedItems = keyEqualityComparer != null
                              ? new ConcurrentDictionaryAdapter<TKey, ICacheItem<TValue>>(keyEqualityComparer)
                              : new ConcurrentDictionaryAdapter<TKey, ICacheItem<TValue>>();

            _cacheItemFactory = cacheItemFactory ?? new DefaultCacheItemFactory();

            _timer.Elapsed += CheckExpiredItems;
            _timer.Start();
        }

        public Cache(TimeSpan timerInterval, IEqualityComparer<TKey> keyEqualityComparer = null, ICacheItemFactory cacheItemFactory = null)
            : this(new TimerAdapter(), new DateTimeProvider(), timerInterval, keyEqualityComparer, cacheItemFactory)
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

        public ICollection<TKey> Keys
        {
            get { return _cachedItems.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _cachedItems.Values.Select(item => item.Value).ToList(); }
        }

        public void Clear()
        {
            ICollection<ICacheItem<TValue>> valueList = _cachedItems.Values;
            _cachedItems.Clear();

            foreach (ICacheItem<TValue> cacheItem in valueList)
            {
                cacheItem.Remove();
            }
        }

        public bool ContainsKey(TKey key)
        {
            return _cachedItems.ContainsKey(key);
        }

        public void Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            ICacheItem<TValue> cacheItem = _cacheItemFactory.CreateInstance(keyValuePair.Value);
            Add(keyValuePair.Key, cacheItem);
        }

        public void Add(TKey key, TValue value)
        {
            ICacheItem<TValue> cacheItem = _cacheItemFactory.CreateInstance(value);
            Add(key, cacheItem);
        }

        public void Add(TKey key, ICacheItem<TValue> cacheItem)
        {
            if (cacheItem == null)
            {
                throw new ArgumentNullException("cacheItem");
            }

            _cachedItems.Add(key, cacheItem);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            ICacheItem<TValue> cacheItem = _cacheItemFactory.CreateInstance(value);
            return TryAdd(key, cacheItem);
        }

        public bool TryAdd(TKey key, ICacheItem<TValue> cacheItem)
        {
            if (cacheItem == null)
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

            if (result)
            {
                value = cacheItemValue.Value;
            }

            return result;
        }

        public bool TryGetValue(TKey key, out ICacheItem<TValue> cacheItem)
        {
            return _cachedItems.TryGetValue(key, out cacheItem);
        }

        public bool TryUpdate(TKey key, TValue newValue)
        {
            return TryUpdate(key, oldCacheItem => oldCacheItem.CreateNewCacheItem(newValue));
        }

        public bool TryUpdate(TKey key, ICacheItem<TValue> newCacheItem)
        {
            return TryUpdate(key, oldCacheItem => newCacheItem);
        }

        private bool TryUpdate(TKey key, Func<ICacheItem<TValue>, ICacheItem<TValue>> updateValueFactory)
        {
            ICacheItem<TValue> currentCacheItem;

            while (_cachedItems.TryGetValue(key, out currentCacheItem))
            {
                if (_cachedItems.TryUpdate(key, updateValueFactory(currentCacheItem), currentCacheItem))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryRemove(TKey key)
        {
            ICacheItem<TValue> cacheItem;
            return _cachedItems.TryRemove(key, out cacheItem);
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            value = default(TValue);
            ICacheItem<TValue> cacheItem;

            if (_cachedItems.TryRemove(key, out cacheItem))
            {
                value = cacheItem.Value;
                return true;
            }

            return false;
        }

        public bool TryRemove(TKey key, out ICacheItem<TValue> value)
        {
            return _cachedItems.TryRemove(key, out value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _cachedItems.Select(item => new KeyValuePair<TKey, TValue>(item.Key, item.Value.Value))
                               .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}