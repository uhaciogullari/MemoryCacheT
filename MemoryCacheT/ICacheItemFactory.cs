namespace MemoryCacheT
{
    /// <summary>
    /// Represents a factory class that will create cache items.
    /// </summary>
    public interface ICacheItemFactory
    {
        /// <summary>
        /// Creates a new cache item with the provided value.
        /// </summary>
        /// <typeparam name="TValue">Type of value in cache item.</typeparam>
        /// <param name="value">Value of the cache item.</param>
        /// <returns>Created cache item.</returns>
        ICacheItem<TValue> CreateInstance<TValue>(TValue value);
    }
}