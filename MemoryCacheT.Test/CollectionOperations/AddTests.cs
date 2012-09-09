using System;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class AddTests : CacheTestBase
    {
        [Test]
        public void Add_KeyDoesntExist_ReturnsTrue()
        {
            _cache.Add(_key, _value);

            Assert.True(_cache.Count > default(int));
        }

        [Test]
        public void Add_KeyExists_ThrowsException()
        {
            _cache.Add(_key, _value);

            Assert.Throws<ArgumentException>(() => _cache.Add(_key, _value));
        }

        [Test]
        public void Add_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Add(null, _value));
        }

        [Test]
        public void AddCacheItem_KeyDoesntExist_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            Assert.True(_cache.Count > default(int));
        }

        [Test]
        public void AddCacheItem_KeyExists_ThrowsException()
        {
            _cache.Add(_key, _cacheItem);

            Assert.Throws<ArgumentException>(() => _cache.Add(_key, new NonExpiringCacheItem<int>(_value)));
        }

        [Test]
        public void AddCacheItem_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Add(_key, null));
        }

        [Test]
        public void AddCacheItem_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Add(null, _cacheItem));
        }
    }
}