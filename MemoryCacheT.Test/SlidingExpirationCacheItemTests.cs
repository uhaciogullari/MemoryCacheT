using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class SlidingExpirationCacheItemTests : CacheItemTestBase
    {
        private DateTime _createDateTime;
        private TimeSpan _cacheInterval;

        protected override ICacheItem<int> CreateCacheItem()
        {
            _createDateTime = Now.AddMinutes(-10);
            DateTimeProviderMock.SetupGet(mock => mock.Now).Returns(_createDateTime);

            _cacheInterval = new TimeSpan(0, 15, 0);

            return new SlidingExpirationCacheItem<int>(DateTimeProviderMock.Object, Value, _cacheInterval);
        }

        [Test]
        public void IsExpired_IntervalHasNotElapsed_ReturnsFalse()
        {
            DateTimeProviderMock.SetupGet(mock => mock.Now).Returns(Now);
            bool isExpired = CacheItem.IsExpired();

            Assert.False(isExpired);
        }

        [Test]
        public void IsExpired_IntervalElapsed_ReturnsTrue()
        {
            DateTime elapsedDateTime = (_createDateTime + _cacheInterval).AddMinutes(5);
            DateTimeProviderMock.SetupGet(mock => mock.Now).Returns(elapsedDateTime);
            bool isExpired = CacheItem.IsExpired();

            Assert.True(isExpired);
        }

        [Test]
        public void IsExpired_ExpirationSlidesAndItIsCheckedWithinRestartedInterval_ReturnsFalse()
        {
            DateTime accessedDateTime = _createDateTime + new TimeSpan(0, 5, 0);
            DateTimeProviderMock.SetupGet(mock => mock.Now).Returns(accessedDateTime);

            int value = CacheItem.Value;

            Now = (_createDateTime + _cacheInterval).AddMinutes(3);
            DateTimeProviderMock.SetupGet(item => item.Now).Returns(Now);

            bool isExpired = CacheItem.IsExpired();
            Assert.False(isExpired);
        }

        [Test]
        public void IsExpired_ExpirationSlidesAndItIsCheckedOutsideRestartedInterval_ReturnsTrue()
        {
            DateTime accessedDateTime = _createDateTime + new TimeSpan(0, 5, 0);
            DateTimeProviderMock.SetupGet(mock => mock.Now).Returns(accessedDateTime);

            int value = CacheItem.Value;

            Now = (_createDateTime + _cacheInterval).AddMinutes(7);
            DateTimeProviderMock.SetupGet(item => item.Now).Returns(Now);

            bool isExpired = CacheItem.IsExpired();
            Assert.True(isExpired);
        }

        public void CreateNewCacheItem_NewValue_ValueIsUpdated()
        {
            int newValue = Value + 7;
            ICacheItem<int> newCacheItem = CacheItem.CreateNewCacheItem(newValue);

            Assert.AreEqual(newValue, newCacheItem.Value);
        }

        [Test]
        public void CreateNewCacheItem_NewValue_OnExpireIsAssigned()
        {
            int newValue = Value + 7;
            Action<int, DateTime> onExpire = (i, time) => { };
            CacheItem.OnExpire = onExpire;

            ICacheItem<int> newCacheItem = CacheItem.CreateNewCacheItem(newValue);

            Assert.AreEqual(onExpire, newCacheItem.OnExpire);
        }

        [Test]
        public void CreateNewCacheItem_NewValue_OnRemoveIsAssigned()
        {
            int newValue = Value + 7;
            Action<int, DateTime> onRemove = (i, time) => { };
            CacheItem.OnRemove = onRemove;

            ICacheItem<int> newCacheItem = CacheItem.CreateNewCacheItem(newValue);

            Assert.AreEqual(onRemove, newCacheItem.OnRemove);
        }

    }
}