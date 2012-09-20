using System;

namespace MemoryCacheT
{
    /// <summary>
    /// Represents a class that returns current time information.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Returns a DateTime representing the current date and time.
        /// </summary>
        DateTime UtcNow { get; }
    }
}