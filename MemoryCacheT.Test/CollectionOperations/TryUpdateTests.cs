using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class TryUpdateTests : CacheTestBase
    {
        [Test]
        public void TryUpdate_KeyExists_ReturnsTrue()
        {
            int newValue = _value + 7;
            _cache.Add(_key, _cacheItem);

            bool result = _cache.TryUpdate(_key, newValue);

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyExists_ValueIsUpdated()
        {
            int newValue = _value + 7;
            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate(_key, newValue);

            int updatedValue;
            bool result = _cache.TryGetValue(_key, out updatedValue) && updatedValue == newValue;

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = _cache.TryUpdate(_key, _value);

            Assert.False(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_ValueIsNotUpdated()
        {
            int newValue = _value + 7;
            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate("invalidKey", newValue);

            int updatedValue;
            bool result = _cache.TryGetValue(_key, out updatedValue) && updatedValue == _value;

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_NewItemIsNotAdded()
        {
            _cache.TryUpdate(_key, _value);

            int updatedValue;
            bool result = _cache.TryGetValue(_key, out updatedValue);

            Assert.False(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyExists_ReturnsTrue()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            _cache.Add(_key, _cacheItem);

            bool result = _cache.TryUpdate(_key, newCacheItem);

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyExists_ValueIsUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate(_key, newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = _cache.TryGetValue(_key, out updatedCacheItem) && updatedCacheItem == newCacheItem;

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            bool result = _cache.TryUpdate(_key, newCacheItem);

            Assert.False(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_ValueIsNotUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);

            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate("invalidKey", newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = _cache.TryGetValue(_key, out updatedCacheItem) && updatedCacheItem == _cacheItem;

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_NewItemIsNotAdded()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            _cache.TryUpdate(_key, newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = _cache.TryGetValue(_key, out updatedCacheItem);

            Assert.False(result);
        }

        [Test]
        public void TryUpdateCacheItem_NewCacheItemIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.TryUpdate(_key, null));
        }
    }
}