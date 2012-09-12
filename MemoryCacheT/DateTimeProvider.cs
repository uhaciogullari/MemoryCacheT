using System;

namespace MemoryCacheT
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        private static Lazy<IDateTimeProvider> __instance =
            new Lazy<IDateTimeProvider>(() => new DateTimeProvider(), true);

        private DateTimeProvider() { }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public static IDateTimeProvider Instance
        {
            get { return __instance.Value; }
        }
    }
}