using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class ContainsTests : CacheTestBase
    {
        private KeyValuePair<string, int> _keyValuePair;

        protected override void FinalizeSetup()
        {
            _keyValuePair = new KeyValuePair<string, int>(_key, _value);
        }

        [Test]
        public void Contains_ItemExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            bool result = _cache.Contains(_keyValuePair);

            Assert.True(result);
        }

        [Test]
        public void Contains_ItemDoesNotExist_ReturnsFalse()
        {
            bool result = _cache.Contains(_keyValuePair);

            Assert.False(result);
        }

        [Test]
        public void Contains_KeyIsNull_ThrowsException()
        {
            KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>(null, _value);

            Assert.Throws<ArgumentNullException>(() => _cache.Contains(keyValuePair));
        }

        [Test]
        public void Contains_ValueIsReferenceType_ChecksForReferenceEquality()
        {
            ICache<string, SampleValue> cache = new Cache<string, SampleValue>(_timerInterval);
            SampleValue value = new SampleValue();
            ICacheItem<SampleValue> cacheItem = new NonExpiringCacheItem<SampleValue>(value);
            KeyValuePair<string, SampleValue> keyValuePair = new KeyValuePair<string, SampleValue>(_key, value);
            cache.Add(_key, cacheItem);

            bool result = cache.Contains(keyValuePair);

            Assert.True(result);
        }

        private class SampleValue { }
    }
}