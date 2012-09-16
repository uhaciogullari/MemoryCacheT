using System;

namespace MemoryCacheT
{
    /// <summary>
    /// Represents an individual cache item.
    /// </summary>
    /// <typeparam name="TValue">Type of value in cache item.</typeparam>
    public interface ICacheItem<TValue>
    {
        /// <summary>
        /// Gets data for the cache item.
        /// </summary>
        TValue Value { get; }
        
        /// <summary>
        /// Creates a new cache item with the same eviction options, using the provided value.
        /// </summary>
        /// <param name="value">Value of the new cache item.</param>
        /// <returns>New cache item.</returns>
        ICacheItem<TValue> CreateNewCacheItem(TValue value);

        /// <summary>
        /// Checks if cache item has expired.
        /// </summary>
        bool IsExpired { get; }

        /// <summary>
        /// Invoked when item is expired.
        /// </summary>
        void Expire();

        /// <summary>
        /// Invoked when item is removed.
        /// </summary>
        void Remove();

        /// <summary>
        /// Defines a reference to a method that is invoked when a cache entry is about to be removed from the cache.
        /// </summary>
        Action<TValue, DateTime> OnExpire { get; set; }

        /// <summary>
        /// Defines a reference to a method that is invoked when a cache entry is about to expire.
        /// </summary>
        Action<TValue, DateTime> OnRemove { get; set; }
    }
}