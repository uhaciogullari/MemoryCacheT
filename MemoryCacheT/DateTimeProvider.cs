using System;

namespace MemoryCacheT
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        private static readonly Lazy<IDateTimeProvider> __instance =
            new Lazy<IDateTimeProvider>(() => new DateTimeProvider(), true);

        private DateTimeProvider() { }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public static IDateTimeProvider Instance
        {
            get { return __instance.Value; }
        }
    }
}