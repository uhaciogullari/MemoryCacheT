using System.Collections.Generic;
using MemoryCacheT.Test.CacheItem;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class RemoveTests : CacheTestBase
    {
        [Test]
        public void Remove_KeyValuePairExistsTest()
        {
            _cache.Add(_key, _value);

            bool result = _cache.Remove(new KeyValuePair<string, int>(_key, _value));

            Assert.True(result);
        }

        [Test]
        public void TryRemove_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            bool result = _cache.Remove(_key);

            Assert.True(result);
        }

        [Test]
        public void TryRemove_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = _cache.Remove(_key);

            Assert.False(result);
        }

        [Test]
        public void TryRemoveGetValue_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            int value;
            bool result = _cache.Remove(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryRemoveGetValue_KeyExists_ReturnsCorrectValue()
        {
            _cache.Add(_key, _cacheItem);

            int value;
            _cache.Remove(_key, out value);

            Assert.AreEqual(_value, value);
        }

        [Test]
        public void TryRemoveGetValue_KeyDoesNotExist_ReturnsFalse()
        {
            int value;
            bool result = _cache.Remove(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryRemoveGetCacheItem_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            ICacheItem<int> cacheItem;
            bool result = _cache.Remove(_key, out cacheItem);

            Assert.True(result);
        }

        [Test]
        public void TryRemoveGetCacheItem_KeyExists_ReturnsCorrectValue()
        {
            _cache.Add(_key, _cacheItem);

            ICacheItem<int> cacheItem;
            _cache.Remove(_key, out cacheItem);

            Assert.AreEqual(_cacheItem, cacheItem);
        }

        [Test]
        public void TryRemoveCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            ICacheItem<int> cacheItem;
            bool result = _cache.Remove(_key, out cacheItem);

            Assert.False(result);
        }
    }
}