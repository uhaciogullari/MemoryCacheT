using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    internal class CacheConstructorTests : CacheTestBase
    {
        [Test]
        public void Constructor_NullTimer_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Cache<string, int>(null, DateTimeProviderMock.Object, TimerInterval));
        }

        [Test]
        public void Constructor_NullDateTimeProvider_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Cache<string, int>(TimerMock.Object, null, TimerInterval));
        }

        [Test]
        public void Constructor_ZeroTimeSpan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Cache<string, int>(TimerMock.Object, DateTimeProviderMock.Object, default(TimeSpan)));
        }

        [Test]
        public void Constructor_Positive_IntervalIsAssignedToTimer()
        {
            TimerMock.SetupSet(mock => mock.Interval = TimerInterval.TotalMilliseconds);
            TimerMock.Setup(mock => mock.Start()).Verifiable();

            new Cache<int, string>(TimerMock.Object, DateTimeProviderMock.Object, TimerInterval);
        }


    }
}