using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    [TestFixture]
    internal class NonExpiringCacheItemTests : CacheItemTestBase
    {
        [Test]
        public void IsExpired_CurrentTimeIsPast_ReturnsFalse()
        {
            bool isExpired = CacheItem.IsExpired;

            Assert.False(isExpired);
        }

        [Test]
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