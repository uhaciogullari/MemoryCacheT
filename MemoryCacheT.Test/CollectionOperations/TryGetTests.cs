using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class TryGetTests : CacheTestBase
    {
        [Test]
        public void TryGet_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);
            int value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGet_KeyDoesNotExist_ReturnFalse()
        {
            int value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGet_NullKey_ThrowsException()
        {
            int value;
            Assert.Throws<ArgumentNullException>(() => _cache.TryGetValue(null, out value));
        }

        [Test]
        public void TryGetCacheItem_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);
            ICacheItem<int> value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGetCacheItem_KeyDoesNotExist_ReturnFalse()
        {
            ICacheItem<int> value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGetCacheItem_NullKey_ThrowsException()
        {
            ICacheItem<int> value;
            Assert.Throws<ArgumentNullException>(() => _cache.TryGetValue(null, out value));
        }

    }
}