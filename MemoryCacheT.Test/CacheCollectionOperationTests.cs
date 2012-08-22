using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class CacheCollectionOperationTests : CacheTestBase
    {
        private int _value;
        private NonExpiringCacheItem<int> _cacheItem;
        private string _key;

        protected override void FinalizeSetup()
        {
            _value = 7;
            _key = "key";
            _cacheItem = new NonExpiringCacheItem<int>(_value);
        }

        [Test]
        public void TryAdd_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.TryAdd(null, _cacheItem));
        }

        [Test]
        public void TryAdd_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.TryAdd(_key, null));
        }

        [Test]
        public void TryAdd_KeyDoesntExist_ReturnsTrue()
        {
            bool isAdded = Cache.TryAdd(_key, _cacheItem);

            Assert.True(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ReturnsFalse()
        {
            Cache.TryAdd(_key, _cacheItem);
            bool isAdded = Cache.TryAdd(_key, _cacheItem);

            Assert.False(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ItemIsNotAdded()
        {
            Cache.TryAdd(_key, _cacheItem);
            Cache.TryAdd(_key, _cacheItem);

            Assert.AreEqual(1, Cache.Count);
        }

        [Test]
        public void TryGet_KeyExists_ReturnsTrue()
        {
            Cache.TryAdd(_key, _cacheItem);
            int value;
            bool result = Cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGet_KeyDoesNotExist_ReturnFalse()
        {
            int value;
            bool result = Cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGet_NullKey_ThrowsException()
        {
            int value;
            Assert.Throws<ArgumentNullException>(() => Cache.TryGetValue(null, out value));
        }


    }
}