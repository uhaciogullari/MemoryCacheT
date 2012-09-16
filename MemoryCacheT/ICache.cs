using System;
using System.Collections.Generic;

namespace MemoryCacheT
{
    /// <summary>
    /// Represents the type that implements an in-memory cache.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the cache.</typeparam>
    /// <typeparam name="TValue">The type of values in the cache.</typeparam>
    public interface ICache<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Adds a cache item with the provided key to the ICache&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="key">The object to use as the key of the cache item to add.</param>
        /// <param name="cacheItem">The cache item to add.</param>
        /// <exception cref="ArgumentNullException">key or cacheItem is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key already exists in the ICache&lt;TKey,TValue&gt;.</exception>
        void Add(TKey key, ICacheItem<TValue> cacheItem);

        /// <summary>
        /// Creates a cache item using the cache item factory and attempts to add it to the ICache&lt;TKey,TValue&gt; with the provided key.
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
        /// <returns>true if the cache item was added to the ICache&lt;TKey,TValue&gt; successfully. If the key already exists, this method returns false.</returns>
        /// <exception cref="ArgumentNullException">key or cacheItem is null.</exception>
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
        /// Compares the value of the existing cache item, which is associated with the provided key, with newValue, and if they are equal, updates the key with the new value by creating a cache item using the cache item factory.
        /// </summary>
        /// <param name="key">The key that will be used to find the cache item to be replaced.</param>
        /// <param name="newValue">The value that will be used to create a new cache item using the cache item factory.</param>
        /// <returns>true if the cache item was replaced by the new one; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        bool TryUpdate(TKey key, TValue newValue);

        /// <summary>
        /// Replaces the existing cache item, which is associated with the provided key, with newCacheItem.
        /// </summary>
        /// <param name="key">The key that will be used to find the cache item to be replaced.</param>
        /// <param name="newCacheItem">Cache item that will replace the old one</param>
        /// <returns>true if the cache item was replaced by the new one; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key or newCacheItem is null.</exception>
        bool TryUpdate(TKey key, ICacheItem<TValue> newCacheItem);

        /// <summary>
        /// Attempts to remove the cache item associated with the provided key from the ICache&lt;TKey,TValue&gt; and return its value.
        /// </summary>
        /// <param name="key">The key of the cache item to remove.</param>
        /// <param name="value"> When this method returns, value contains the value of the cache item removed from the ICache&lt;TKey,TValue&gt; or the default value of TValue if the operation failed.</param>
        /// <returns>true if a cache item was removed successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        bool Remove(TKey key, out TValue value);

        /// <summary>
        /// Attempts to remove and return the cache item associated with the provided key from the ICache&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="key">The key of the cache item to remove.</param>
        /// <param name="cacheItem">When this method returns, cacheItem contains the cache item removed from the ICache&lt;TKey,TValue&gt; or null if the operation failed.</param>
        /// <returns>true if a cache item was removed successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        bool Remove(TKey key, out ICacheItem<TValue> cacheItem);
    }
}