using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class ValuesTests : CacheTestBase
    {
        private string _key1;
        private string _key2;
        private string _key3;
        
        private int _value1;
        private int _value2;
        private int _value3;

        private NonExpiringCacheItem<int> _cacheItem1;
        private NonExpiringCacheItem<int> _cacheItem2;
        private NonExpiringCacheItem<int> _cacheItem3;

        protected override void FinalizeSetup()
        {
            _key1 = "key1";
            _key2 = "key2";
            _key3 = "key3"; 
            
            _value1 = _value + 1;
            _value2 = _value + 2;
            _value3 = _value + 3;

            _cacheItem1 = new NonExpiringCacheItem<int>(_value1);
            _cacheItem2 = new NonExpiringCacheItem<int>(_value2);
            _cacheItem3 = new NonExpiringCacheItem<int>(_value3);
        }

        [Test]
        public void Values_CacheIsNotEmpty_ChangesAreNotReflected()
        {
            AddItems();
            ICollection<int> values = _cache.Values;
            _cache.Add(_key, _cacheItem);

            bool result = values.Contains(_value);

            Assert.False(result);
        }

        [Test]
        public void Values_Always_ReturnsReadOnlyCollection()
        {
            AddItems();
            ICollection<int> values = _cache.Values;

            Assert.True(values.IsReadOnly);
        }

        [Test]
        public void Values_Always_AddingItemsThrowsException()
        {
            AddItems();
            ICollection<int> values = _cache.Values;

            Assert.Throws<NotSupportedException>(() => values.Add(_value));
        }

        [Test]
        public void Values_WhenCacheIsEmpty_ReturnsEmptyCollection()
        {
            ICollection<int> values = _cache.Values;

            Assert.AreEqual(default(int), values.Count);
        }

        private void AddItems()
        {
            _cache.Add(_key1, _cacheItem1);
            _cache.Add(_key2, _cacheItem2);
            _cache.Add(_key3, _cacheItem3);
        }
    }
}