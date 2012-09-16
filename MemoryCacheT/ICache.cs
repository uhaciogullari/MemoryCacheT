using System;
using System.Collections.Generic;

namespace MemoryCacheT
{
    /// <summary>
    /// Represents a generic collection of cached items stored in memory.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the cache.</typeparam>
    /// <typeparam name="TValue">The type of values in the cache.</typeparam>
    public interface ICache<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Adds an cache item with the provided key to the ICache&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="key">The object to use as the key of the cache item to add.</param>
        /// <param name="cacheItem">The cache item to add.</param>
        /// <exception cref="ArgumentNullException">key or the cacheItem is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key already exists in the ICache&lt;TKey,TValue&gt;.</exception>
        void Add(TKey key, ICacheItem<TValue> cacheItem);

        /// <summary>
        /// Creates a cache item using cache item factory and attempts to add the created cache item to the ICache&lt;TKey,TValue&gt; with the provided key.
        /// </summary>
        /// <param name="key">The object to use as the key of the cache item to add.</param>
        /// <param name="value">The object to use as the value of the cache item to add.</param>
        /// <returns>true if the cache item was added to the ICache&lt;TKey,TValue&gt; successfully. If the key already exists, this method returns false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        bool TryAdd(TKey key, TValue value);

        /// <summary>
        /// Attemps to add a cache item with the provided key to the ICache&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="key">The object to use as the key of the cache item to add.</param>
        /// <param name="cacheItem">The cache item to add.</param>
        /// <returns>An element with the same key already exists in the ICache&lt;TKey,TValue&gt;.</returns>
        /// <exception cref="ArgumentNullException">key or the cacheItem is null.</exception>
        bool TryAdd(TKey key, ICacheItem<TValue> cacheItem);
        
        /// <summary>
        /// Attempts to get the cache item associated with the specified key from the ICache&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="key">The key of the cache item to get.</param>
        /// <param name="cacheItem">When this method returns, value contains the cache item from the ICache&lt;TKey,TValue&gt; with the specified key or null , if the operation failed.</param>
        /// <returns>true if the key was found in the ICache&lt;TKey,TValue&gt; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        bool TryGetValue(TKey key, out ICacheItem<TValue> cacheItem);
        
        /// <summary>
        /// Compares the existing value of the cache item associated with the provided key with a specified value, and if they are equal, updates the key with the new value by creating a cache item using the cache item factory.
        /// </summary>
        /// <param name="key">The key that will be used to find the cache item to be replaced.</param>
        /// <param name="newValue">The value that will be used to create a new cache item using the cache item factory.</param>
        /// <returns>true if the cache item was replaced by the new one; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        bool TryUpdate(TKey key, TValue newValue);

        /// <summary>
        /// Compares references of the existing cache item associated with the provided key and newCacheItem, if they are equal, updates the key with newCacheItem.
        /// </summary>
        /// <param name="key">The key that will be used to find the cache item to be replaced.</param>
        /// <param name="newCacheItem">Cache item that will replace the old one</param>
        /// <returns>true if the cache item was replaced by the new one; otherwise, false.</returns>
        bool TryUpdate(TKey key, ICacheItem<TValue> newCacheItem);

        bool Remove(TKey key, out TValue value);
        bool Remove(TKey key, out ICacheItem<TValue> cacheItem);
    }
}