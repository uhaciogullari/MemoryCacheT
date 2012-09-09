using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    internal class CacheConstructorTests : CacheTestBase
    {
        [Test]
        public void Constructor_NullTimer_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Cache<string, int>(null, _timerInterval));
        }

        [Test]
        public void Constructor_ZeroTimeSpan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Cache<string, int>(_timerMock.Object, default(TimeSpan)));
        }

        [Test]
        public void Constructor_Positive_IntervalIsAssignedToTimer()
        {
            _timerMock.SetupSet(mock => mock.Interval = _timerInterval.TotalMilliseconds);
            _timerMock.Setup(mock => mock.Start()).Verifiable();

            new Cache<int, string>(_timerMock.Object, _timerInterval);
        }


    }
}