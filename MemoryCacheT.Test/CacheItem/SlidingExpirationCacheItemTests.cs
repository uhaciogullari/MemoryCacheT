using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    [TestFixture]
    internal class SlidingExpirationCacheItemTests : CacheItemTestBase
    {
        private DateTime _createDateTime;
        private TimeSpan _cacheInterval;

        protected override ICacheItem<int> CreateCacheItem()
        {
            _createDateTime = _now.AddMinutes(-10);
            _dateTimeProviderMock.SetupGet(mock => mock.UtcNow).Returns(_createDateTime);

            _cacheInterval = new TimeSpan(0, 15, 0);

            return new SlidingExpirationCacheItem<int>(_dateTimeProviderMock.Object, _value, _cacheInterval);
        }

        [Test]
        public void IsExpired_IntervalHasNotElapsed_ReturnsFalse()
        {
            _dateTimeProviderMock.SetupGet(mock => mock.UtcNow).Returns(_now);
            bool isExpired = _cacheItem.IsExpired;

            Assert.False(isExpired);
        }

        [Test]
        public void IsExpired_IntervalElapsed_ReturnsTrue()
        {
            DateTime elapsedDateTime = (_createDateTime + _cacheInterval).AddMinutes(5);
            _dateTimeProviderMock.SetupGet(mock => mock.UtcNow).Returns(elapsedDateTime);
            bool isExpired = _cacheItem.IsExpired;

            Assert.True(isExpired);
        }

        [Test]
        public void IsExpired_ExpirationSlidesAndItIsCheckedWithinRestartedInterval_ReturnsFalse()
        {
            DateTime accessedDateTime = _createDateTime + new TimeSpan(0, 5, 0);
            _dateTimeProviderMock.SetupGet(mock => mock.UtcNow).Returns(accessedDateTime);

            int value = _cacheItem.Value;

            _now = (_createDateTime + _cacheInterval).AddMinutes(3);
            _dateTimeProviderMock.SetupGet(item => item.UtcNow).Returns(_now);

            bool isExpired = _cacheItem.IsExpired;
            Assert.False(isExpired);
        }

        [Test]
        public void IsExpired_ExpirationSlidesAndItIsCheckedOutsideRestartedInterval_ReturnsTrue()
        {
            DateTime accessedDateTime = _createDateTime + new TimeSpan(0, 5, 0);
            _dateTimeProviderMock.SetupGet(mock => mock.UtcNow).Returns(accessedDateTime);

            int value = _cacheItem.Value;

            _now = (_createDateTime + _cacheInterval).AddMinutes(7);
            _dateTimeProviderMock.SetupGet(item => item.UtcNow).Returns(_now);

            bool isExpired = _cacheItem.IsExpired;
            Assert.True(isExpired);
        }

        [Test]
        public void CreateNewCacheItem_NewValue_ValueIsUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = _cacheItem.CreateNewCacheItem(newValue);

            Assert.AreEqual(newValue, newCacheItem.Value);
        }

        [Test]
        public void CreateNewCacheItem_NewValue_OnExpireIsAssigned()
        {
            int newValue = _value + 7;
            Action<int, DateTime> onExpire = (i, time) => { };
            _cacheItem.OnExpire = onExpire;

            ICacheItem<int> newCacheItem = _cacheItem.CreateNewCacheItem(newValue);

            Assert.AreEqual(onExpire, newCacheItem.OnExpire);
        }

        [Test]
        public void CreateNewCacheItem_NewValue_OnRemoveIsAssigned()
        {
            int newValue = _value + 7;
            Action<int, DateTime> onRemove = (i, time) => { };
            _cacheItem.OnRemove = onRemove;

            ICacheItem<int> newCacheItem = _cacheItem.CreateNewCacheItem(newValue);

            Assert.AreEqual(onRemove, newCacheItem.OnRemove);
        }

    }
}