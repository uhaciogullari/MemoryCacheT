using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    [TestFixture]
    internal class AbsoluteExpirationCacheItemTests : CacheItemTestBase
    {
        private DateTime _expirationDate;

        protected override ICacheItem<int> CreateCacheItem()
        {
            _expirationDate = _now.AddDays(1);
            return new AbsoluteExpirationCacheItem<int>(_dateTimeProviderMock.Object, _value, _expirationDate);
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
            _now = _expirationDate.AddDays(1);
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