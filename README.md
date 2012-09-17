MemoryCacheT
============

A thread-safe dictionary with eviction options. MemoryCacheT simply wraps System.Collections.Concurrent.ConcurrentDictionary class and offers some options for expiring elements.

    //check for expired items every 10 seconds
    TimeSpan timerInterval = new TimeSpan(0, 0, 10);
    
    //create cache
    ICache<string, int> cache = new Cache<string, int>(timerInterval);
    
    //add items
    cache.Add("key", new SlidingExpirationCacheItem<int>(value : 7, cacheInterval: new TimeSpan(0, 1, 0)));