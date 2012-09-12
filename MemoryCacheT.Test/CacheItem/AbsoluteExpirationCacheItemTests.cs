using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    [TestFixture]
    internal class AbsoluteExpirationCacheItemTests : CacheItemTestBase
    {
        private DateTime _expirationDateTime;
        private DateTime _createDateTime;

        protected override ICacheItem<int> CreateCacheItem()
        {
            _createDateTime = _now.AddMinutes(-5);
            _expirationDateTime = _now.AddDays(1);
            _dateTimeProviderMock.SetupGet(item => item.Now).Returns(_createDateTime);

            return new AbsoluteExpirationCacheItem<int>(_dateTimeProviderMock.Object, _value, _expirationDateTime);
        }

        [Test]
        public void Constructor_ExpirationDateIsEarlierThanNow_ThrowsException()
        {
            DateTime expirationDate = _now.AddMinutes(-1);
            _dateTimeProviderMock.SetupGet(item => item.Now).Returns(_now);

            Assert.Throws<ArgumentException>(
                () => new AbsoluteExpirationCacheItem<int>(_dateTimeProviderMock.Object, _value, expirationDate));
        }

        [Test]
        public void Constructor_CacheIntervalIsGiven_IntervalStartsFromCurrentDateTime()
        {
            TimeSpan cacheInterval = new TimeSpan(0, 5, 0);
            _dateTimeProviderMock.SetupGet(item => item.Now).Returns(_now);

            ICacheItem<int> cacheItem = new AbsoluteExpirationCacheItem<int>(_dateTimeProviderMock.Object, _value, cacheInterval);

            _dateTimeProviderMock.SetupGet(item => item.Now).Returns(_now + cacheInterval);

            Assert.True(cacheItem.IsExpired);
        }

        [Test]
        public void IsExpired_CurentDateTimeIsLessThanExpirationDate_ReturnsFalse()
        {
            _dateTimeProviderMock.SetupGet(item => item.Now).Returns(_now);
            bool isExpired = _cacheItem.IsExpired;

            Assert.False(isExpired);
        }

        [Test]
        public void IsExpired_CurentDateTimeIsGreaterThanExpirationDate_ReturnsTrue()
        {
            _now = _expirationDateTime.AddDays(1);
            _dateTimeProviderMock.SetupGet(item => item.Now).Returns(_now);
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