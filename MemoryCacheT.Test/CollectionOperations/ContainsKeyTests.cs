using System;
using MemoryCacheT.Test.CacheItem;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class ContainsKeyTests : CacheTestBase
    {
        [Test]
        public void ContainsKey_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            bool result = _cache.ContainsKey(_key);

            Assert.True(result);
        }

        [Test]
        public void ContainsKey_KeyDoesNotExist_ReturnsTrue()
        {
            bool result = _cache.ContainsKey(_key);

            Assert.False(result);
        }

        [Test]
        public void ContainsKey_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.ContainsKey(null));
        }
    }
}