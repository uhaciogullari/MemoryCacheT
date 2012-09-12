using System;

namespace MemoryCacheT
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}