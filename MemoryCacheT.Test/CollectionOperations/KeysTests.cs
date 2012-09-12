using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class KeysTests : CacheTestBase
    {
        private string _key1;
        private string _key2;
        private string _key3;

        protected override void FinalizeSetup()
        {
            _key1 = "key1";
            _key2 = "key2";
            _key3 = "key3";
        }

        [Test]
        public void Keys_CacheIsNotEmpty_ChangesAreNotReflected()
        {
            AddItems();
            ICollection<string> keys = _cache.Keys;
            _cache.Add(_key, new NonExpiringCacheItem<int>(_value));

            bool result = keys.Contains(_key);

            Assert.False(result);
        }

        [Test]
        public void Keys_Always_ReturnsReadOnlyCollection()
        {
            AddItems();
            ICollection<string> keys = _cache.Keys;

            Assert.True(keys.IsReadOnly);
        }

        [Test]
        public void Keys_Always_AddingItemsThrowsException()
        {
            AddItems();
            ICollection<string> keys = _cache.Keys;

            Assert.Throws<NotSupportedException>(() => keys.Add(_key));
        }

        [Test]
        public void Keys_WhenCacheIsEmpty_ReturnsEmptyCollection()
        {
            ICollection<string> keys = _cache.Keys;

            Assert.AreEqual(default(int), keys.Count);
        }

        private void AddItems()
        {
            _cache.Add(_key1, new NonExpiringCacheItem<int>(_value));
            _cache.Add(_key2, new NonExpiringCacheItem<int>(_value));
            _cache.Add(_key3, new NonExpiringCacheItem<int>(_value));
        }
    }
}