using System;

namespace MemoryCacheT
{
    internal interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}