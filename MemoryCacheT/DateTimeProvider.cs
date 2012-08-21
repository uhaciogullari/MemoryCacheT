using System;

namespace MemoryCacheT
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}