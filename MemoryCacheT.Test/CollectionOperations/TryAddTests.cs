using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class TryAddTests : CacheTestBase
    {
        [Test]
        public void TryAdd_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.TryAdd(null, _cacheItem));
        }

        [Test]
        public void TryAdd_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.TryAdd(_key, null));
        }

        [Test]
        public void TryAdd_KeyDoesntExist_ReturnsTrue()
        {
            bool isAdded = _cache.TryAdd(_key, _cacheItem);

            Assert.True(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ReturnsFalse()
        {
            _cache.Add(_key, _cacheItem);
            bool isAdded = _cache.TryAdd(_key, _cacheItem);

            Assert.False(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ItemIsNotAdded()
        {
            _cache.Add(_key, _cacheItem);
            _cache.TryAdd(_key, _cacheItem);

            Assert.AreEqual(1, _cache.Count);
        }
    }
}